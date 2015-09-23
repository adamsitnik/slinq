namespace Slinq.Profiling
{
    /// <summary>
    /// VS profiler does not support Suspend & Resume for Sampling Mode, hence the class does nothing
    /// </summary>
    public class SamplingProfilingApi : IProfilingApi
    {
        public void SuspendProfiling()
        {
        }

        public void ResumeProfiling()
        {
        }

        public void StopProfiling()
        {
        }
    }
}