using System;
using Slinq.Benchmarks;
using Slinq.Profiling.Profiled;

namespace Slinq.Profiling.Profiles
{
    public class DateTimeSystemSorterBasedOnOperatorsProfile : ProfilingTemplate<DateTimeSystemSorterBasedOnOperators, DateTime[]>
    {
        private readonly int _elementsCount;

        public DateTimeSystemSorterBasedOnOperatorsProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override DateTimeSystemSorterBasedOnOperators CreateProfiledObject()
        {
            return new DateTimeSystemSorterBasedOnOperators();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(DateTimeSystemSorterBasedOnOperators profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData, 0, inputData.Length - 1);

            return inputData;
        }
    }
}