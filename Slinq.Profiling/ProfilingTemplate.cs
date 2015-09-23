using System;

namespace Slinq.Profiling
{
    public abstract class ProfilingTemplate<TProfiled, TInput>
    {
        private readonly IProfilingApi _profilingApi;

        protected ProfilingTemplate(IProfilingApi profilingApi)
        {
            _profilingApi = profilingApi;
        }

        public void Profile()
        {
            _profilingApi.SuspendProfiling();

            var profiledObject = CreateProfiledObject();
            var warmUpInputData = CreateInputData();

            var jitWarmUp = Execute(profiledObject, warmUpInputData);

            var inputData = CreateInputData();

            _profilingApi.ResumeProfiling();

            var theRealProfiling = Execute(profiledObject, inputData);
            
            _profilingApi.StopProfiling();

            MakeReleaseBuildNotOmitTheResults(jitWarmUp, theRealProfiling);
        }

        protected abstract TProfiled CreateProfiledObject();

        protected abstract TInput CreateInputData();

        protected abstract object Execute(TProfiled profiledObject, TInput inputData);

        private static void MakeReleaseBuildNotOmitTheResults(object jitWarmUp, object theRealProfiling)
        {
            if (ReferenceEquals(jitWarmUp, theRealProfiling))
            {
                Console.WriteLine("Just using the results");
            }
        }
    }
}