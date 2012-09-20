using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SchedulingBenchmarking
{
    class BenchmarkSystem
    {
        Scheduler scheduler = new Scheduler();
        public event EventHandler<StateChangedEventArgs> StateChanged;
        public String[] Status;
<<<<<<< HEAD
=======
        
        
>>>>>>> e6df398efeedacb09999b74e1b411d4049420136

        static void Main(String[] args) 
        {
            /*
            BenchmarkSystem system = new BenchmarkSystem();
            system.Submit(new Job(new Owner("me"), 10));   
         */

            //////////////////////////////////////////////

            BenchmarkSystem system = new BenchmarkSystem();
            Logger.Subscribe(system);

<<<<<<< HEAD

            Job test = new Job((string[] arg) => { foreach (string s in arg) { Console.Out.WriteLine(s); } return ""; }, new Owner("dsad"), 3);
=======
            String[] input = {"eeng", "mrof", "cstp"};
            Func<string[], int> Process = (input) => 34; 

            Job test = new Job();
>>>>>>> e6df398efeedacb09999b74e1b411d4049420136

            system.Submit(test);
            system.ExecuteAll();
        }

        public BenchmarkSystem()
        {
            
        }

        /*** Methods ***/
        
        public void Submit(Job job)
        {
            OnChanged(new StateChangedEventArgs() { State = State.Submitted });
            scheduler.addJob(job);
        }

        public void Cancel(Job job)
        {
            OnChanged(new StateChangedEventArgs() { State = State.Cancelled });
            scheduler.removeJob(job);
        }

        public void ExecuteAll()
        {

            while (!scheduler.Empty) {
                Job job = scheduler.popJob();
                String result = job.Process(new string[] {"Processing job started at: "+ job.TimeAdded });
                // event started
                OnChanged(new StateChangedEventArgs() { State = State.Running });
                if (result == null) OnChanged(new StateChangedEventArgs() { State = State.Failed }); // if failed
                else OnChanged(new StateChangedEventArgs() { State = State.Terminated }); // when finished
            }    

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

            private Job getNewestJob()
            {

                var timedJobs = new Job[] {ShortQueue.OrderBy(j => j.TimeAdded).Last(),
                                           MediumQueue.OrderBy(j => j.TimeAdded).Last(), 
                                           LongQueue.OrderBy(j => j.TimeAdded).Last() };

                return timedJobs.OrderBy(j => j.TimeAdded).Last();
            }

            internal Job removeJob(Job job)
            {
                int time = job.ExpectedRuntimeMinutes;

                if (time < 30)
                    return ShortQueue.Dequeue();

                if (time <= 30 && time < 120)
                    return MediumQueue.Dequeue();

                if (time <= 120)
                    return LongQueue.Dequeue();

                return null;
            }

            internal Job popJob()
            {

                Job newestJob = getNewestJob();

                return removeJob(newestJob);
            }
        }
    }
}
