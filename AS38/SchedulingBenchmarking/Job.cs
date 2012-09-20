using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    /// <summary>
    /// This class represents a job to be executed.
    /// </summary>
    class Job
    {
        // a delegate for the processing method
        private Func<string[], string> del;
        public int ExpectedRuntime;
        public DateTime TimeAdded;
        public Owner Owner;
        public int CPUsNeeded
        {
            get;
            set 
            { 
                if (value >= 1 && value < 7) 
                    CPUsNeeded = value; 
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="del">The function that are executed when job.process is called</param>
        /// <param name="owner"> the owner of the job</param>
        /// <param name="cpus">number of cpus required</param>
        /// <param name="expectedRuntime">the expected runtime in minutes</param>
        public Job(Func<string[], string> del, Owner owner, int cpus, int expectedRuntime)
        {
            this.del = del;
            this.Owner = owner;
            this.CPUsNeeded = cpus;
            TimeAdded = new DateTime();
            this.ExpectedRuntime = expectedRuntime;

        }
        /// <summary>
        /// This method calls the supplied delegate method. 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Process(string[] args)
        {
            return del(args);
        }


        public override string toString() 
        { 
            return "Job added: " +TimeAdded+" Owner 
        }

    }

    
}
