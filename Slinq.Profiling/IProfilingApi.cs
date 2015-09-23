namespace Slinq.Profiling
{
    public interface IProfilingApi
    {
        void SuspendProfiling();

        void ResumeProfiling();

        void StopProfiling();
    }
}