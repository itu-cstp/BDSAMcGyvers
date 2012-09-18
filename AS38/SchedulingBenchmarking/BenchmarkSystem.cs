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

        static void Main(String[] args) 
        {
            BenchmarkSystem bench = new BenchmarkSystem();
            bench.Submit(new Job(new Owner("me"), 10));
            
        }

        public BenchmarkSystem()
        {
            this.JobSubmittet += new EventHandler(giveMessage);
        }

        public void Submit(Job job)
        {
            onSubmittet(EventArgs.Empty);
        }

        public void Cancel(Job job)
        {

        }

        public void ExecuteAll()
        {

        }

        public String[] Status;        

        private void onSubmittet(EventArgs e)
        {

            if (JobSubmittet != null) JobSubmittet(this, e);
                
        }

        public void giveMessage(object sender, EventArgs e)
        {
            Console.Out.WriteLine("message received");
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
