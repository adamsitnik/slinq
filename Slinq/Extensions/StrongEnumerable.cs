using Slinq.Iterators;

namespace Slinq.Extensions
{
    public static class StrongEnumerable
    {
        public static RangeIterator Range(int start, int count)
        {
            return new RangeIterator(start, count);
        }
    }
}