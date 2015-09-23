using System;
using Slinq.Benchmarks;
using Slinq.Profiling.Profiled;

namespace Slinq.Profiling.Profiles
{
    public class DateTimeSystemSorterProfile : ProfilingTemplate<DateTimeSystemSorter, DateTime[]>
    {
        private readonly int _elementsCount;

        public DateTimeSystemSorterProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override DateTimeSystemSorter CreateProfiledObject()
        {
            return new DateTimeSystemSorter();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(DateTimeSystemSorter profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData, 0, inputData.Length - 1);

            return inputData;
        }
    }
}