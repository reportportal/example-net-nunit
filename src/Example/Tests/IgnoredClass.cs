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
    [TestFixture, Ignore("Ignore reason for suite")]
    [Description("All tests in this suite should be skipped.")]
    public class IgnoredClass
    {
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
