using System;
using Slinq.Benchmarks;
using Slinq.Profiling.Profiled;

namespace Slinq.Profiling.Profiles
{
    public class PreloadedDynamicSorterNoRefProfile : ProfilingTemplate<PreloadedDynamicDateTimeArraySorterNoRef, DateTime[]>
    {
        private readonly int _elementsCount;

        public PreloadedDynamicSorterNoRefProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override PreloadedDynamicDateTimeArraySorterNoRef CreateProfiledObject()
        {
            return new PreloadedDynamicDateTimeArraySorterNoRef();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(PreloadedDynamicDateTimeArraySorterNoRef profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData);

            return inputData;
        }
    }
}