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
    [Category("Category 1"), Category("Category 2")]
    [TestFixture]
    [Description("This suite throws an exception in OneTimeSetup")]
    public class FailedClass
    {
        [OneTimeSetUp]
        public void Init()
        {
            throw new Exception("Here is OneTimeSetup exception.");
        }

        [Test]
        public void Test1()
        {
            
        }

        [Test]
        public void Test2()
        {

        }
    }
}
