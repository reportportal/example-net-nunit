using NUnit.Framework;
using ReportPortal.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests.InnerFolder
{
    [TestFixture]
    public class Class1
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            throw new Exception("OneTimeSetUp exception.");
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
