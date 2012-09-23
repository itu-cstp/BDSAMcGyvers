using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchedulingBenchmarking
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestOnStateChanged()
        {
            Logger.OnStateChanged(new String(new Char[] {'s','e','n','d','e','r'}), new StateChangedEventArgs() { State = State.Failed });
            // assert that a message is printed....
        }
    }
}
