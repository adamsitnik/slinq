using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Slinq.Utils
{
    /// <summary>
    /// this class is getting the arrays from collections that are just wrapping them
    /// in some tricky way, these are private fields ofc
    /// 
    /// possible enhancement: 
    ///     1) save the generated CIL to .il file, create module out of it and reference it
    ///     gains: bigger reusability (.NET native does not support reflection) + performance
    /// this class is rather a PoC for now
    /// </summary>
    internal static class ArrayProvider<T>
    {
        /// <summary>
        /// source: http://referencesource.microsoft.com/#mscorlib/system/collections/generic/list.cs,2765070d40f47b98
        /// </summary>
        private const string ListFieldNameThatContainsArray = "_items";

        private static readonly FieldInfo ListsArrayField = typeof(List<T>).GetField(ListFieldNameThatContainsArray, BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly Func<List<T>, T[]> ArrayFromListGetter = GenerateArrayFromListGetter();

        [Obsolete("Don't use, 33 TIMES slower that GetWrappedArrayWithDynamicCilGeneration")]
        internal static T[] GetWrappedArrayWithReflection(List<T> source)
        {
            return (T[])ListsArrayField.GetValue(source);
        }

        internal static T[] GetWrappedArrayWithDynamicCilGeneration(List<T> source)
        {
            return ArrayFromListGetter.Invoke(source);
        }

        private static Func<List<T>, T[]> GenerateArrayFromListGetter()
        {
            var dynamicMethod = new DynamicMethod(
                "GetArray", 
                typeof(T[]), 
                new[] { typeof(List<T>) },
                owner: typeof(List<T>), // required for private field access
                skipVisibility: true); // very important, needed to suspend rights check

            var cilGenerator = dynamicMethod.GetILGenerator();
            cilGenerator.Emit(OpCodes.Ldarg_0); // load the argument (list)
            cilGenerator.Emit(OpCodes.Ldfld, ListsArrayField); // load the field
            cilGenerator.Emit(OpCodes.Ret); // return field

            return (Func<List<T>, T[]>)dynamicMethod.CreateDelegate(typeof(Func<List<T>, T[]>));
        }
    }
}