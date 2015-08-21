using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Slinq.Models;

namespace Slinq.Utils
{
    /// <summary>
    /// this class allows to get encapsulated array from wrapping collection
    /// 
    /// possible enhancement: 
    ///     1) save the generated CIL to .il file, create module out of it and reference it
    ///     gains: bigger reusability (.NET native does not support reflection) + performance
    /// </summary>
    internal static class ArrayProvider<T>
    {
        private static readonly Func<List<T>, T[]> ArrayFromListGetter
            = CilGenerator.GenerateGetter<List<T>, T[]>(ReverseEngineering<T>.ListsArrayField);

        private static readonly Func<ReadOnlyCollection<T>, IList<T>> IListFromReadOnlyCollectionGetter
            = CilGenerator.GenerateGetter<ReadOnlyCollection<T>, IList<T>>(ReverseEngineering<T>.ReadonlyCollectionIListField);

        [Obsolete("Don't use, 33 TIMES slower that GetWrappedArray that uses dynamic Cil generation")]
        internal static T[] GetWrappedArrayWithReflection(List<T> source)
        {
            return (T[])ReverseEngineering<T>.ListsArrayField.GetValue(source);
        }

        internal static T[] GetWrappedArray(List<T> source)
        {
            return ArrayFromListGetter.Invoke(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ExtractedArray<T> Extract(T[] source)
        {
            return new ExtractedArray<T>(source, source.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ExtractedArray<T> Extract(List<T> source)
        {
            return new ExtractedArray<T>(GetWrappedArray(source), source.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ExtractedArray<T> Extract(ReadOnlyCollection<T> source)
        {
            return new ExtractedArray<T>(GetWrappedArray(source), source.Count);
        }

        internal static T[] GetWrappedArray(ReadOnlyCollection<T> source)
        {
            var wrappedCollection = IListFromReadOnlyCollectionGetter.Invoke(source);

            var array = wrappedCollection as T[];
            if (array != null)
            {
                return array;
            }

            var list = wrappedCollection as List<T>;
            if (list != null)
            {
                return GetWrappedArray(list);
            }

            throw new NotSupportedException("Only ReadOnlyCollections that wrap Arrays or Lists are supported");
        }
    }
}