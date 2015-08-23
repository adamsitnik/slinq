using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Slinq.Models;
using Slinq.Utils;

namespace Slinq.ImmutableCollections.Utils
{
    internal static class ArrayProvider<T>
    {
        private static readonly Func<ImmutableArray<T>, T[]> ArrayFromImmutableArrayGetter;

        static ArrayProvider()
        {
            try
            {
                ArrayFromImmutableArrayGetter = CilGenerator.GenerateGetter<ImmutableArray<T>, T[]>(
                    ReverseEngineering<T>.ImmutableArrayArrayField);
            }
            catch (SecurityException ex)
            {
                if (ex.PermissionType != typeof(ReflectionPermission))
                {
                    throw;
                }

                // TODO: need to find another hack for this or just refuse support!
                ArrayFromImmutableArrayGetter = immutableArray => immutableArray.ToArray();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ExtractedArray<T> Extract(ImmutableArray<T> source)
        {
            return new ExtractedArray<T>(ArrayFromImmutableArrayGetter(source), source.Length);
        }
    }
}