using Slinq.Profiling.Profiles;

namespace Slinq.Profiling._64bit
{
    class Program
    {
        static void Main(string[] args)
        {
            new SystemSorterProfile(new InstrumentationProfilingApi(), 1000);
        }
    }
}
