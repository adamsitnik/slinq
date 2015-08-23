using System;
using System.Collections.Generic;

namespace Slinq.Utils
{
    /// <summary>
    /// this class allows to create List from Array without copying the data! 
    /// (List created via standard ctor from IEnumerable copies old array into new)
    /// the array MUST be created by us, it CAN NOT come from the outside world!
    /// </summary>
    internal static class ListFactory<T>
    {
        private static readonly Action<List<T>, T[]> ListsPrivateArraySetter
            = CilGenerator.GenerateSetter<List<T>, T[]>(ReverseEngineering<T>.ListsArrayField);

        private static readonly Action<List<T>, int> ListsPrivateSizeSetter
            = CilGenerator.GenerateSetter<List<T>, int>(ReverseEngineering<T>.ListsSizeField);

        internal static List<T> Create(T[] array)
        {
            var list = new List<T>(0);
            if (array.Length <= 0)
            {
                return list;
            }

            ListsPrivateArraySetter(list, array);
            ListsPrivateSizeSetter(list, array.Length);

            return list;
        }
    }
}