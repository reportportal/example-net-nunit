using NUnit.Framework;

namespace Example.Tests
{
    [Parallelizable]
    
    [Description("It is Description of Fixture from Class4")]
    [TestFixture(TestName = "Class4, It is TestName of TestFixture")]
    public class Class4
    {

        [TestCase]
        public void Test1()
        {
            ReportPortal.Shared.Context.Current.Log.Info("TestCase with Empty [TestCase] attribute");
        }

        [Description("It is description of TestCase")]
        [TestCase]
        public void Test2()
        {
            ReportPortal.Shared.Context.Current.Log.Info("TestCase with Description for [TestCase] attribute");
        }

        [TestCase(TestName = "It is TestName of TestCase")]
        public void Test3()
        {
            ReportPortal.Shared.Context.Current.Log.Info("TestCase with TestName in [TestCase] attribute");
        }

        [TestCase("1", TestName = "It is TestName of TestCase with 1")]
        [TestCase("2", TestName = "It is TestName of TestCase with 2")]
        [TestCase("3", TestName = "It is TestName of TestCase with 3")]
        public void Test4(string testParam)
        {
            ReportPortal.Shared.Context.Current.Log.Info("TestCase with TestName in [TestCase] attribute");
            ReportPortal.Shared.Context.Current.Log.Info($"TestCase with TestParam {testParam}");
        }
    }
}
