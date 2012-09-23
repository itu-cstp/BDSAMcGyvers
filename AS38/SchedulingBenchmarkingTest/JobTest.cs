using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchedulingBenchmarking
{
    [TestClass]
    public class JobTest
    {
        [TestMethod]
        public void TestToString()
        {
            Job job = new Job((string[] arg) => { return arg.Length.ToString(); }, new Owner("tester"), 3, 35);
            Assert.AreEqual("Job added: 01-01-0001 00:00:00 owner: tester",job.ToString());
        }
    }
}
