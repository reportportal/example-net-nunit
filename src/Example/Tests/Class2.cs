using NUnit.Framework;
using ReportPortal.Shared;
using System;
using System.Collections;
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
        }

        [Test]
        public void Test2()
        {

        }

        [Test]
        public void Test3()
        {
            Assert.Fail("test failed.");
        }

        [Test, Description("My static description, and this test was executed on {MachineName} machine.")]
        public void Test4()
        {
            
        }

        [Test, Ignore("My ignore reason")]
        public void IgnoredTest()
        {
            
        }

        [Test]
        [Description("This test should have actual unique ID in logs assigned by server")]
        public void SyncTest()
        {

        }

        [Test, TestCaseSource(typeof(Class2), nameof(Source))]
        public void TestWithTestCaseSource(int i)
        {

        }

        private static IEnumerable Source
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new TestCaseData(i);
                }
            }
        }
    }
}
