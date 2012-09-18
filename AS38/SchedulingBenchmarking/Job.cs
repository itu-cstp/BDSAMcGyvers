using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class Job
    {
        public int CPUsNeeded;

        public int ExpectedRuntimeMinutes;

        //public State State;

        public Owner Owner;

        public Job(Owner o, int time)
        {
            Owner = o;
            ExpectedRuntimeMinutes = time;
        }

        public void Process()
        {
            // run for x nr of minutes
        }
    }
}
