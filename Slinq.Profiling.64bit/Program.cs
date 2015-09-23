using Slinq.Profiling.Profiles;

namespace Slinq.Profiling._64bit
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new DisplayTimeProfilingApi();
            const int count = 1500000;

            new PreloadedDynamicSorterRefBasedProfile(api, count).Profile();
            new PreloadedDynamicSorterNoRefProfile(api, count).Profile();
            new GenericSystemSorterProfile(api, count).Profile();
            new DateTimeSystemSorterBasedOnCompareToProfile(api, count).Profile();
            new DateTimeSystemSorterBasedOnOperatorsProfile(api, count).Profile();
            new DynamicSorterProfile(api, count).Profile();
        }
    }
}
