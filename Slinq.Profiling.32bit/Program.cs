using Slinq.Profiling.Profiles;

namespace Slinq.Profiling._32bit
{
    class Program
    {
        static void Main(string[] args)
        {
            new SystemSorterProfile(new InstrumentationProfilingApi(), 1000);
        }
    }
}
