using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class Job
    {
        public int CPUsNeeded
        {
            get;
            set 
            { 
                if (value >= 1 && value < 7) 
                    CPUsNeeded = value; 
            }
        }

        public int ExpectedRuntimeMinutes;

        public DateTime TimeAdded;

        public State State;

        public Owner Owner;

        public Job(Owner o, int time)
        {
            Owner = o;
            ExpectedRuntimeMinutes = time;
            TimeAdded = DateTime.Now;
        }

        public void Process(string[] args)
        {
            // run for x nr of minutes
            //(args) => Console.WriteLine("{0} Job will run for {1} minutes", args, ExpectedRuntimeMinutes);
        }
    }
}
