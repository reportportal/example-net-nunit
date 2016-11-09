using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    [Parallelizable]
    [TestFixture]
    public class Class1
    {
        [Category("T1")]
        [Test]
        public void Test1()
        {
            Console.WriteLine("QWEQWEQWE");
            Console.WriteLine("ASDASD");
            System.Threading.Thread.Sleep(3000);
        }

        [Test]
        public void Test2()
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}
