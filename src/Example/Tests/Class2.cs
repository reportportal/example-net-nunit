using NUnit.Framework;
using ReportPortal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests
{
    [Parallelizable]
    [Category("Category 1"), Category("Category 2")]
    [TestFixture]
    public class Class2
    {
        [Test]
        public void Test1()
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Trace, "class2 test1 log message");

            System.Threading.Thread.Sleep(2000);
        }

        [Test]
        public void Test2()
        {
            System.Threading.Thread.Sleep(6000);
        }

        [Test]
        public void Test3()
        {
            System.Threading.Thread.Sleep(7000);
            Assert.Fail("test failed.");
        }
    }
}
