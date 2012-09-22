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
        //The shedular that holds incomming jobs. 
        Scheduler scheduler = new Scheduler();

        // eventhandler that fires event on stateChange
        public event EventHandler<StateChangedEventArgs> StateChanged;        

        public HashSet<Job>[] Status;
        HashSet<Job> StatusRunning;
        HashSet<Job> StatusQueuing;

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
            Status = new HashSet<Job>[2];
            StatusRunning = new HashSet<Job>();
            StatusQueuing = new HashSet<Job>();
            Status[0] = StatusRunning;
            Status[1] = StatusQueuing;
        }

        /*** Methods ***/
        
        public void Submit(Job job)
        {
            changeState(job, State.Submitted);
            scheduler.addJob(job);
            StatusQueuing.Add(job);
        }

        public void Cancel(Job job)
        {
            changeState(job, State.Cancelled);
            scheduler.removeJob(job);
            StatusQueuing.Remove(job);
        }

        public void ExecuteAll()
        {
            while (!scheduler.Empty()) {
             
                // start job
                Job job = scheduler.popJob();
                changeState(job, State.Running);
                StatusQueuing.Remove(job);
                StatusRunning.Add(job);

                String result = job.Process(new string[] { "Processing job started at: " + job.TimeAdded });

                // if failed
                if (result == null)
                {
                    changeState(job, State.Failed);
                    StatusRunning.Remove(job);
                }
                // when finished
                else
                {
                    changeState(job, State.Terminated);
                    StatusRunning.Remove(job);
                }
            }    
        }

        private void changeState(Job job, State state)
        {
            job.State = state;
            OnChanged(new StateChangedEventArgs() { State = state });
        }

        private void OnChanged(StateChangedEventArgs e)
        {
            if (StateChanged != null)
            {
                StateChanged(this, e);
            }
        }     

        public void giveMessage(object sender, EventArgs e)
        {
            Console.WriteLine("message received");
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
