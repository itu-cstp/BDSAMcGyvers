using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class Logger
    {
        public void OnJobSubmitter()
        {
            Console.WriteLine("job submitted");
        }
        public void OnJobCancelled()
        {
            Console.WriteLine("job cancelled");
        }
        public void OnJobRunning()
        {
            Console.WriteLine("job running");
        }
        public void OnJobTerminated()
        {
            Console.WriteLine("job terminated");
        }
        public void OnJobFailed()
        {
            Console.WriteLine("job failed");
        }
    }
}
