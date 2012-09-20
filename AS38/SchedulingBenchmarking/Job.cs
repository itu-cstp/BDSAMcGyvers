using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class Job
    {
        private int CPUsNeeded;
        private Func<string[], string> del;
        public DateTime Time;
        public Owner Owner;

        public Job(Func<string[], string> del, Owner owner, int cpus)
        {
            this.del = del;
            this.Owner = owner;
            this.CPUsNeeded = cpus;
            Time = new DateTime();
        }

        public string Process(string[] args)
        {
            return del(args);
            
        }
    }

    
}
