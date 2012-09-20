using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class Logger
    {
        /// <summary>
        /// Method invoked by any state change in BenchMarkSystem. Publishes a running commentary 
        /// when any job is submitted, cancelled, run, failed or terminated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Console.WriteLine("Sender: {0} job state {1}", sender, e.State);
        }
    }
}
