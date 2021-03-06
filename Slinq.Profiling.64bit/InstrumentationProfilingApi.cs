﻿using System;
using Microsoft.VisualStudio.Profiler;

namespace Slinq.Profiling._64bit
{
    public class InstrumentationProfilingApi : IProfilingApi
    {
        public void SuspendProfiling()
        {
            DataCollection.SuspendProfile(ProfileLevel.Thread, DataCollection.CurrentId);
        }

        public void ResumeProfiling()
        {
            DataCollection.ResumeProfile(ProfileLevel.Thread, DataCollection.CurrentId);
        }

        public void StopProfiling(Type profilingTemplate)
        {
            DataCollection.StopProfile(ProfileLevel.Thread, DataCollection.CurrentId);
        }
    }
}