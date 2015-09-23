using System;
using Slinq.Abstract;
using Slinq.Benchmarks;
using Slinq.Utils;

namespace Slinq.Profiling.Profiles
{
    public class DynamicSorterProfile : ProfilingTemplate<IArraySorter<DateTime>, DateTime[]>
    {
        private readonly int _elementsCount;

        public DynamicSorterProfile(IProfilingApi profilingApi, int elementsCount) : base(profilingApi)
        {
            _elementsCount = elementsCount;
        }

        protected override IArraySorter<DateTime> CreateProfiledObject()
        {
            return DedicatedSortersFactory.CreateDedicatedSorter<DateTime>();
        }

        protected override DateTime[] CreateInputData()
        {
            return DataGenerator.GenerateRandomDates(_elementsCount);
        }

        protected override object Execute(IArraySorter<DateTime> profiledObject, DateTime[] inputData)
        {
            profiledObject.Sort(inputData);

            return inputData;
        }
    }
}