namespace Slinq.Abstract
{
    public interface ICopyFreeComparer<T>
        where T : struct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", 
            Justification = "This helps us to avoid memory copying for Value Types")]
        int Compare(ref T left, ref T right);
    }
}