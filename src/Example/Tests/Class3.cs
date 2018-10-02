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
    [TestFixture]
    public class Class3
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            throw new Exception("Setup exception.");
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
