using System;
using Slinq.Benchmarks;
using Slinq.Profiling.Profiled;

namespace Slinq.Profiling.Profiles
{
    public class AlreadyLoadedDynamicSorterProfile : ProfilingTemplate<DateTimeArraySorter, DateTime[]>
    {
        private readonly int _elementsCount;

        public AlreadyLoadedDynamicSorterProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override DateTimeArraySorter CreateProfiledObject()
        {
            return new DateTimeArraySorter();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(DateTimeArraySorter profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData);

            return inputData;
        }
    }
}