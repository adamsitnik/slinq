using System.Collections.Immutable;
using System.Reflection;

namespace Slinq.ImmutableCollections.Utils
{
    internal static class ReverseEngineering<T>
    {
        internal static readonly FieldInfo ImmutableArrayArrayField
            = typeof(ImmutableArray<T>).GetField(ImmutableArrayFieldNameThatContainsArray, BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// source: https://github.com/dotnet/corefx/blob/master/src/System.Collections.Immutable/src/System/Collections/Immutable/ImmutableArray%601.cs
        /// </summary>
        private const string ImmutableArrayFieldNameThatContainsArray = "array";
    }
}