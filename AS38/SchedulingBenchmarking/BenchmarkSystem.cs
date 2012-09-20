using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SchedulingBenchmarking
{
    class BenchmarkSystem
    {
        Scheduler scheduler = new Scheduler();
        public event EventHandler JobSubmittet;
        public event EventHandler<StateChangedEventArgs> StateChanged;
        public String[] Status;  

        static void Main(String[] args) 
        {
            /*
            BenchmarkSystem system = new BenchmarkSystem();
            system.Submit(new Job(new Owner("me"), 10));   
         */

            //////////////////////////////////////////////

            BenchmarkSystem system = new BenchmarkSystem();
            Logger.Subscribe(system);

            Job test = new Job(new Owner("me"), 5);

            system.Submit(test);
            system.ExecuteAll();
        }

        public BenchmarkSystem()
        {
            //this.JobSubmittet += new EventHandler(giveMessage);
        }

        /*** Methods ***/
        
        public void Submit(Job job)
        {
            OnChanged(new StateChangedEventArgs() { State = State.Submitted });
        }

        public void Cancel(Job job)
        {
            OnChanged(new StateChangedEventArgs() { State = State.Cancelled });
        }

        public void ExecuteAll()
        {
            // when started
            OnChanged(new StateChangedEventArgs() { State = State.Running });

            // if failed
            OnChanged(new StateChangedEventArgs() { State = State.Failed });

            // when finished
            OnChanged(new StateChangedEventArgs() { State = State.Terminated });
        }

        /*** Events ***/

        private void OnChanged(StateChangedEventArgs e)
        {
            if (StateChanged != null)
            {
                StateChanged(this, e);
            }
        }     

        private void onSubmittet(EventArgs e)
        {

            if (JobSubmittet != null) JobSubmittet(this, e);
                
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

            internal Scheduler()
            {
                ShortQueue = new Queue<Job>();
                MediumQueue = new Queue<Job>();
                LongQueue = new Queue<Job>();
            }

            internal void addJob(Job job)
            {
                int time = job.ExpectedRuntimeMinutes;

                if (time < 30) 
                    ShortQueue.Enqueue(job);

                if (time <= 30 && time < 120) 
                    MediumQueue.Enqueue(job);
                
                if (time <= 120) 
                    LongQueue.Enqueue(job);
            }

            internal void removeJob(Job job)
            {

            }

            internal Job popJob()
            {
                return new Job(new Owner("me"), 10);
            }

        }
    }
}
