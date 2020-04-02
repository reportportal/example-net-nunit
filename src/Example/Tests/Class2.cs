using NUnit.Framework;
using ReportPortal.Shared;
using System.Collections;
using System.Threading;
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
            ReportPortal.Shared.Log.Trace("class2 test1 log message");
        }

        [Test]
        public void Test2()
        {
            Assert.Warn("My warn message");
        }

        [Test]
        public void Test3()
        {
            Assert.Fail("test failed.");
        }

        [Test]
        public void Test3FailedWithoutMessage()
        {
            Assert.Fail();
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

        [Test, TestCaseSource(typeof(Class2), nameof(Source)), Parallelizable(ParallelScope.Children)]
        public async Task TestWithTestCaseSource(int input)
        {
            await Task.Delay(1000);
            for (int i = 0; i < 2; i++)
            {
                using (var scope = Log.BeginNewScope($"Scope {i}"))
                {
                    for (int j = 0; j < 2; j++)
                    {
                        await Task.Delay(1);
                        scope.Info($"Log {j}");
                    }

                    await Task.Delay(1);

                    using (var scope2 = Log.BeginNewScope("Scope Level 2"))
                    {
                        Log.Debug("Level 2 message");

                        scope2.Status = ReportPortal.Shared.Logging.LogScopeStatus.Failed;
                    }
                }
            }
        }

        private static IEnumerable Source
        {
            get
            {
                for (int i = 0; i < 30; i++)
                {
                    yield return new TestCaseData(i);
                }
            }
        }
    }
}
