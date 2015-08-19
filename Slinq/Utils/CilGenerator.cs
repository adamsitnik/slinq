using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Slinq.Utils
{
    internal static class CilGenerator
    {
        internal static Func<TOwner, TReturn> GenerateGetter<TOwner, TReturn>(
            FieldInfo fieldInfo)
        {
            var dynamicMethod = new DynamicMethod(
                "GetEncapsulatedField",
                typeof(TReturn),
                new[] { typeof(TOwner) },
                owner: typeof(TOwner), // required for private field access
                skipVisibility: true); // very important, needed to suspend rights check

            var cilGenerator = dynamicMethod.GetILGenerator();
            cilGenerator.Emit(OpCodes.Ldarg_0); // load the argument (list)
            cilGenerator.Emit(OpCodes.Ldfld, fieldInfo); // load the field
            cilGenerator.Emit(OpCodes.Ret); // return field

            return (Func<TOwner, TReturn>)dynamicMethod
                .CreateDelegate(typeof(Func<TOwner, TReturn>));
        }
    }
}