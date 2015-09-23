using System;
using System.Diagnostics;

namespace Slinq.Profiling
{
    public class DisplayTimeProfilingApi : IProfilingApi
    {
        private Stopwatch _watch;

        public void SuspendProfiling()
        {
        }

        public void ResumeProfiling()
        {
            _watch = Stopwatch.StartNew();
        }

        public void StopProfiling(Type profilingTemplate)
        {
            _watch.Stop();

            Console.WriteLine("{0} for {1}", _watch.Elapsed, profilingTemplate.Name);
        }
    }
}