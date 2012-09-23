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
        private Scheduler scheduler = new Scheduler();

        // Eventhandler that fires event on stateChange
        public event EventHandler<StateChangedEventArgs> StateChanged;        

        // HashSet that contains a list of all submitted and running jobs, but not cancelled, failed, or terminated jobs
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
            if (state == State.Cancelled) Status.Remove(job);
            if (state == State.Failed) Status.Remove(job);
            if (state == State.Terminated) Status.Remove(job);
            // if state changes from submitted to running, the object 
            // will change state, but the HashSet won't need updating.
        }

     
    }
}
