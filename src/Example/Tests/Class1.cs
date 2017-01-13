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
    [TestFixture]
    [Description("this is description for Class1")]
    public class Class1
    {
        [Category("T1")]
        [Test]
        public void Test1()
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Trace, "class1 test1 log message");
            System.Threading.Thread.Sleep(3000);
        }

        [Test]
        [Description("this is description for Test2")]
        public void Test2()
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}
