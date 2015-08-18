namespace Slinq.Abstract
{
    public interface IStrongEnumerator<out T>
    {
        T Current { get; }

        bool MoveNext();
    }
}