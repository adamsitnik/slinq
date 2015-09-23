using System;
using Slinq.Benchmarks;
using Slinq.Profiling.Profiled;

namespace Slinq.Profiling.Profiles
{
    public class DateTimeSystemSorterBasedOnCompareToProfile : ProfilingTemplate<DateTimeSystemSorterBasedOnCompareTo, DateTime[]>
    {
        private readonly int _elementsCount;

        public DateTimeSystemSorterBasedOnCompareToProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override DateTimeSystemSorterBasedOnCompareTo CreateProfiledObject()
        {
            return new DateTimeSystemSorterBasedOnCompareTo();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(DateTimeSystemSorterBasedOnCompareTo profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData, 0, inputData.Length - 1);

            return inputData;
        }
    }
}