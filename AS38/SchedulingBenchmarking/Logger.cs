using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    public class Logger
    {
        /// <summary>
        /// Method invoked by any state change in BenchMarkSystem. Publishes a running commentary 
        /// when any job is submitted, cancelled, run, failed or terminated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Console.WriteLine("Job state {0}", e.State);
        }
    }
}
