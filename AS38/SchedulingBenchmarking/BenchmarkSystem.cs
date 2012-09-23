using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SchedulingBenchmarking
{   
    /// <summary>
    /// This class processes created jobs, fires events at job-start,end,fail and houses the job scheduler.
    /// Currently it doesn't really perform any benchmarks.
    /// 
    /// </summary>
    class BenchmarkSystem
    {   

        //The sheduler that holds incoming jobs. 
        Scheduler scheduler = new Scheduler();

        // Eventhandler that fires event on stateChange
        public event EventHandler<StateChangedEventArgs> StateChanged;        

        // HashSet that contains a list of all submitted and running jobs, 
        // but not cancelled, failed, or terminated jobs
        public HashSet<Job> Status;

        static void Main(String[] args) 
        {
            
            BenchmarkSystem system = new BenchmarkSystem();

            // get the logger to subscribe to BenchmarkSystem
            system.StateChanged += Logger.OnStateChanged;
            
            Job test = new Job((string[] arg) => { foreach (string s in arg) { Console.Out.WriteLine(s); } return ""; }, new Owner("dsad"), 3,3);
            
            system.Submit(test);
            system.ExecuteAll();
        }

        public BenchmarkSystem()
        {
            Status = new HashSet<Job>();
        }

        /*** Methods ***/
        
        public void Submit(Job job)
        {
            changeState(job, State.Submitted);
            scheduler.addJob(job);
        }

        public void Cancel(Job job)
        {
            changeState(job, State.Cancelled);
            scheduler.removeJob(job);
        }

        public void ExecuteAll()
        {
            while (!scheduler.Empty()) {
             
                // get job from scheduler
                Job job = scheduler.popJob();

                // start job
                changeState(job, State.Running);
                String result = job.Process(new string[] { "Processing job started at: " + job.TimeAdded });

                // if failed
                if (result == null)
                {
                    changeState(job, State.Failed);
                }

                // when finished
                else
                {
                    changeState(job, State.Terminated);
                }
            }    
        }

        private void changeState(Job job, State state)
        {
            job.State = state;
            fireEvent(new StateChangedEventArgs() { State = state });
            updateStatus(job);
        }

        private void fireEvent(StateChangedEventArgs e)
        {
            if (StateChanged != null) StateChanged(this, e);
        }

        private void updateStatus(Job job)
        {
            State state = job.State;
            if (state == State.Submitted) Status.Add(job);
            else if (state == State.Cancelled) Status.Remove(job);
            else if (state == State.Failed) Status.Remove(job);
            else if (state == State.Terminated) Status.Remove(job);
            // if state changes from submitted to running, the object 
            // will change state, but the HashSet won't need updating.
        }

        /// <summary>
        /// Internal class that handles scheduling of tasks. 
        /// </summary>
        private class Scheduler
        {
            Queue<Job> ShortQueue;
            Queue<Job> MediumQueue;
            Queue<Job> LongQueue;

            HashSet<Job> removedJobs;

            internal Scheduler()
            {
                ShortQueue = new Queue<Job>();
                MediumQueue = new Queue<Job>();
                LongQueue = new Queue<Job>();
                removedJobs = new HashSet<Job>();
            }

            internal void addJob(Job job)
            {
                int time = job.ExpectedRuntime;

                if (time < 30) 
                    ShortQueue.Enqueue(job);

                if (time <= 30 && time < 120) 
                    MediumQueue.Enqueue(job);
                
                if (time <= 120) 
                    LongQueue.Enqueue(job);
            }

            /// <summary>
            /// Method to get the newest job between the three queues. 
            /// It does so by simoply querying for their respective newest job
            /// </summary>
            /// <todo> Could be a one-liner ?</todo>
            /// <returns> The newest job </returns>
            private Job getNewestJob()
            {
                /// Create an array of the three times
                var timedJobs = new Job[] {ShortQueue.OrderBy(j => j.TimeAdded).Last(),
                                           MediumQueue.OrderBy(j => j.TimeAdded).Last(), 
                                           LongQueue.OrderBy(j => j.TimeAdded).Last() };
                
                /// Return the most recent of the previously found three values
                return timedJobs.OrderBy(j => j.TimeAdded).Last();
            }

            /// <summary>
            /// "Remove a job" in the sense that we are not really removing anything at this stage.
            /// Simply mark it as removed and check for it when popping
            /// </summary>
            /// <param name="job"> Job to remove</param>
            internal void removeJob(Job job)
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
            internal Job popJob()
            {

                Job newestJob = getNewestJob();

                // Criteria for which queue to pop from
                int time = newestJob.ExpectedRuntime;

                // Object that we will return
                Job popped = null;

                /*
                 * Look at the time! And determine which queue is suitable
                 */
                if (time < 30)
                    popped = ShortQueue.Dequeue();

                if (time <= 30 && time < 120)
                    popped = MediumQueue.Dequeue();

                if (time <= 120)
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
            internal bool Empty(){
                return (ShortQueue.Count < 1 && MediumQueue.Count < 1 && LongQueue.Count < 1);
            }
        }
    }
}
