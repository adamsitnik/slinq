using Slinq.Profiling.Profiles;

namespace Slinq.Profiling._32bit
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new DisplayTimeProfilingApi();

            //new DateTimeSystemSorterProfile(api, 1500000).Profile();
            new AlreadyLoadedDynamicSorterProfile(api, 1500000).Profile();
            //new SystemSorterProfile(api, 1500000).Profile();
        }
    }
}
