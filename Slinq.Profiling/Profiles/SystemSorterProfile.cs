using System;
using Slinq.Benchmarks;
using Slinq.Utils;

namespace Slinq.Profiling.Profiles
{
    public class SystemSorterProfile : ProfilingTemplate<SystemSorter<DateTime>, DateTime[]>
    {
        private readonly int _elementsCount;

        public SystemSorterProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override SystemSorter<DateTime> CreateProfiledObject()
        {
            return new SystemSorter<DateTime>();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(SystemSorter<DateTime> profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData, 0, inputData.Length - 1);

            return inputData;
        }
    }
}