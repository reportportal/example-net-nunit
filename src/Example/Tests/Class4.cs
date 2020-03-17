using NUnit.Framework;

namespace Example.Tests
{
    [Parallelizable]
    
    [Description("It is Description of Fixture from Class4")]
    [TestFixture(TestName = "It is TestName of TestFixture")]
    public class Class4
    {

        [TestCase]
        public void Test1()
        {
            ReportPortal.Shared.Log.Info("TestCase with Empty [TestCase] attribute");
        }

        [Description("It is description of TestCase")]
        [TestCase]
        public void Test2()
        {
            ReportPortal.Shared.Log.Info("TestCase with Description for [TestCase] attribute");
        }

        [TestCase(TestName = "It is TestName of TestCase")]
        public void Test3()
        {
            ReportPortal.Shared.Log.Info("TestCase with TestName in [TestCase] attribute");
        }
    }
}
