using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Slinq.Utils
{
    internal static class ReverseEngineering<T>
    {
        /// <summary>
        /// source: http://referencesource.microsoft.com/#mscorlib/system/collections/generic/list.cs,2765070d40f47b98
        /// </summary>
        private const string ListFieldNameThatContainsArray = "_items";

        /// <summary>
        /// source: http://referencesource.microsoft.com/#mscorlib/system/collections/generic/list.cs,aeb6ba6c11713802
        /// </summary>
        private const string ListFieldNameThatContainsSize = "_size";

        /// <summary>
        /// source: http://referencesource.microsoft.com/#mscorlib/system/collections/objectmodel/readonlycollection.cs,633b76eb3a793621
        /// </summary>
        private const string ReadonlyCollectionFieldNameThatContainsIList = "list";

        internal static readonly FieldInfo ListsArrayField
            = typeof(List<T>).GetField(ListFieldNameThatContainsArray, BindingFlags.NonPublic | BindingFlags.Instance);

        internal static readonly FieldInfo ListsSizeField
            = typeof(List<T>).GetField(ListFieldNameThatContainsSize, BindingFlags.NonPublic | BindingFlags.Instance);

        internal static readonly FieldInfo ReadonlyCollectionIListField
            = typeof(ReadOnlyCollection<T>).GetField(ReadonlyCollectionFieldNameThatContainsIList, BindingFlags.NonPublic | BindingFlags.Instance);
    }
}