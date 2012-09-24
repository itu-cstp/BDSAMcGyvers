using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    /// <summary>
    /// Internal class that handles scheduling of tasks. 
    /// </summary>
    public class Scheduler
    {
        Queue<Job> ShortQueue;
        Queue<Job> MediumQueue;
        Queue<Job> LongQueue;

        HashSet<Job> removedJobs;

        //singleton field
        private static Scheduler instance = new Scheduler();

        private Scheduler()
        {
            ShortQueue = new Queue<Job>();
            MediumQueue = new Queue<Job>();
            LongQueue = new Queue<Job>();
            removedJobs = new HashSet<Job>();
        }

        public void addJob(Job job)
        {   

            // FIX to be able to compare timestamps.
            System.Threading.Thread.Sleep(1);
            job.TimeAdded = DateTime.Now.Ticks;
            int time = job.ExpectedRuntime;

            if (time < 30)
                ShortQueue.Enqueue(job);

            if (time >= 30 && time < 120)
                MediumQueue.Enqueue(job);

            if (time >= 120)
                LongQueue.Enqueue(job);
        }

        /// <summary>
        /// Method to get the newest job between the three queues. 
        /// It does so by simoply querying for their respective newest job
        /// NOTE: henter den ikke "OldestJob" altså det job, der blev added først???? I så fald er det en misvisende titel.
        /// </summary>
        /// <todo> Could be a one-liner ?</todo>
        /// <returns> The newest job </returns>
        private Job getNewestJob()
        {
            /// Create a list of the three times
            List<Job> timedJob = new List<Job>();
            if (ShortQueue.Count > 0) 
                timedJob.Add(ShortQueue.ElementAt(0));
            if (MediumQueue.Count > 0)
                timedJob.Add(MediumQueue.ElementAt(0));
            if (LongQueue.Count > 0)
                timedJob.Add(LongQueue.ElementAt(0));

            /// Return the most recent of the previously found three values
            return timedJob.OrderBy(job => job.TimeAdded).ElementAt(0);
        }

        /// <summary>
        /// "Remove a job" in the sense that we are not really removing anything at this stage.
        /// Simply mark it as removed and check for it when popping
        /// </summary>
        /// <param name="job"> Job to remove</param>
        public void removeJob(Job job)
        {
            removedJobs.Add(job);
        }

        /// <summary>
        /// Function to "pop", return and remove the newest element
        /// Do so by finding the newest job between the three queues. 
        /// Check if we have marked that job for removal. If so do not return it, but instead return the next job
        /// 
        /// </summary>
        /// <returns> The popped job or null if there's no job to return</returns>
        public Job popJob()
        {

            Job newestJob = getNewestJob();

            // Object that we will return
            Job popped = null;

            /*
             * Look at the time! And determine which queue is suitable
             */
            if (newestJob.ExpectedRuntime < 30)
                popped = ShortQueue.Dequeue();

            else if (newestJob.ExpectedRuntime >= 30 && newestJob.ExpectedRuntime < 120)
                popped = MediumQueue.Dequeue();

            else if (newestJob.ExpectedRuntime >= 120)
                popped = LongQueue.Dequeue();

            // Check if the popped job is actually a removed one. 
            // If so we should remove the mark and recursively return the next job in line
            if (removedJobs.Contains(popped))
            {
                removedJobs.Remove(newestJob);
                return popJob();
            }
            return popped;
        }

        /// <summary>
        /// Simple method to check whether the three queues are all empty
        /// </summary>
        /// <returns> Boolean if all queues are empty</returns>
        public bool Empty()
        {
            return ((ShortQueue.Count == 0) && (MediumQueue.Count == 0) && (LongQueue.Count == 0));
        }

        public static Scheduler getInstance() 
        {
            return instance;
        }
    }
}
