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

        public void StopProfiling()
        {
            _watch.Stop();

            Console.WriteLine("Total elapsed time: {0}", _watch.Elapsed);
        }
    }
}