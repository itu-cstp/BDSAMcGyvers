using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    class StateChangedEventArgs : EventArgs
    {
        public State State
        {
            get;
            set;
        }
    }	
}
