using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class Logger
    {
        public static void Subscribe(BenchmarkSystem system)
        {
            system.StateChanged += OnStateChanged;
        }

        public static void UnSubscribe(BenchmarkSystem system)
        {
            system.StateChanged -= OnStateChanged;
        }

        private static void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Console.WriteLine("Sender: {0} job state {1}", sender, e.State);
        }    
    }
}
