using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class Job
    {
        private Func<string[], string> del;
        public int ExpectedRuntime;
        public DateTime TimeAdded;
        private int cpusneeded;

        public int CPUsNeeded
        {
            get 
            { 
                return cpusneeded; 
            }
            set 
            { 
                if (value >= 1 && value < 7) 
                    cpusneeded = value; 
            }
        }
 
        public State State;

        public Owner Owner;

        public Job(Func<string[], string> del, Owner owner, int cpus, int expectedRuntime)
        {
            this.del = del;
            this.Owner = owner;
            this.CPUsNeeded = cpus;
            TimeAdded = new DateTime();
            this.ExpectedRuntime = expectedRuntime;
        }

        public string Process(string[] args)
        {
            return del(args);
        }
    }    
}
