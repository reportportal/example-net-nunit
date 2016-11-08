using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    [Parallelizable]
    [Category("TF2")]
    [TestFixture]
    public class Class2
    {
        [Test]
        public void Test1()
        {
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
        }
    }
}
