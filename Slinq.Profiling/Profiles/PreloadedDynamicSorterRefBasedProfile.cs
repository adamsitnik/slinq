using System;
using Slinq.Benchmarks;
using Slinq.Profiling.Profiled;

namespace Slinq.Profiling.Profiles
{
    public class PreloadedDynamicSorterRefBasedProfile : ProfilingTemplate<PreloadedDynamicDateTimeArraySorterRefBased, DateTime[]>
    {
        private readonly int _elementsCount;

        public PreloadedDynamicSorterRefBasedProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override PreloadedDynamicDateTimeArraySorterRefBased CreateProfiledObject()
        {
            return new PreloadedDynamicDateTimeArraySorterRefBased();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(PreloadedDynamicDateTimeArraySorterRefBased profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData);

            return inputData;
        }
    }
}