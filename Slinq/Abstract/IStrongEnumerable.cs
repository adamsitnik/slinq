namespace Slinq.Abstract
{
    public interface IStrongEnumerable<out T, TEnumerator>
        where TEnumerator : struct, IStrongEnumerator<T> // due to struct limitation we can avoid boxing on interface calls on structs 
    {
        TEnumerator GetEnumerator();
    }
}