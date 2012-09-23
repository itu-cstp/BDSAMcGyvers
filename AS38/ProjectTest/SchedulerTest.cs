using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulingBenchmarking;

namespace SchedulingBenchmarkingTest
{
    [TestClass]
    public class SchedulerTest
    {
        internal Scheduler scheduler; 
        internal Job job1;
        internal Job job2;
        internal Job job3;

        [TestInitialize]
        public void Initialize() {
            scheduler = Scheduler.getInstance();
            job1 = new Job((string[] args) => { foreach (string s in args) { Console.Out.WriteLine(s); } return ""; }, new Owner("owner1"), 2,3);
            job2 = new Job((string[] args) => { foreach (string s in args) { Console.Out.WriteLine(s); } return ""; }, new Owner("owner2"), 2, 45);
            job3 = new Job((string[] args) => { foreach (string s in args) { Console.Out.WriteLine(s); } return ""; }, new Owner("owner3"), 2, 200);
        }

        [TestMethod]
        public void TestAddJob()
        {
            scheduler.addJob(job1);
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job1.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job2.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job3.Owner.Name);
        }

        [TestMethod]
        public void TestEmpty()
        {
            Assert.IsTrue(scheduler.Empty());
            scheduler.addJob(job1);
            Assert.IsFalse(scheduler.Empty());
            scheduler.popJob();
            Assert.IsTrue(scheduler.Empty());
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            Assert.IsFalse(scheduler.Empty());
            scheduler.popJob();
            scheduler.popJob();
            Assert.IsTrue(scheduler.Empty());
        }

        [TestMethod]
        public void TestRemoveJob()
        {
            scheduler.addJob(job1);
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            scheduler.removeJob(job2);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job1.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job3.Owner.Name);
            Assert.IsTrue(scheduler.Empty());
        }

        [TestMethod]
        public void PopJob()
        {
            scheduler.addJob(job1);
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            scheduler.addJob(job1);
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            scheduler.addJob(job1);
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            scheduler.addJob(job1);
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            scheduler.addJob(job1);
            scheduler.addJob(job2);
            scheduler.addJob(job3);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job1.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job2.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job3.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job1.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job2.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job3.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job1.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job2.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job3.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job1.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job2.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job3.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job1.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job2.Owner.Name);
            Assert.AreEqual(scheduler.popJob().Owner.Name, job3.Owner.Name);
            Assert.IsTrue(scheduler.Empty());
        }
    }
}
