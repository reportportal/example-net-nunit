using NUnit.Framework;
using ReportPortal.Shared;
using ReportPortal.Shared.Execution.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Example.Tests
{
    [Parallelizable]
    [TestFixture]
    [Description("this is description for Class1")]
    public class Class1
    {
        protected log4net.ILog Log4NetLogger = log4net.LogManager.GetLogger(typeof(Class1));

        [Category("T1")]
        [Test]
        public void Test1()
        {
            ReportPortal.Shared.Context.Current.Log.Trace("class1 test1 log message");
            Log4NetLogger.Info("My log message from Log4Net");
            var filePath = TestContext.CurrentContext.TestDirectory + "\\dog.png";

            // add attachment into results
            TestContext.AddTestAttachment(filePath, "my dog");

            // or send directly to ReportPortal
            ReportPortal.Shared.Context.Current.Log.Info("my dog {rp#file#" + filePath + "}");

            var jsonBase64 = Convert.ToBase64String(Encoding.Default.GetBytes("{a: true}"));
            ReportPortal.Shared.Context.Current.Log.Info("my json {rp#base64#application/json#" + jsonBase64 + "}");
        }

        [Test]
        [Description("this is description for Test2")]
        public void Test2()
        {
            Thread.Sleep(1000);

            ReportPortal.Shared.Context.Current.Log.Trace("class1 test2 log message");

            Thread.Sleep(1000);

            using (var scope = ReportPortal.Shared.Context.Current.Log.BeginScope("qwe"))
            {
                Thread.Sleep(500);
                scope.Debug("inner qwe");
                Thread.Sleep(500);
            }

            Thread.Sleep(3000);

            using (var scope = ReportPortal.Shared.Context.Current.Log.BeginScope("qwe"))
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1);
                    scope.Debug("inner qwe " + i);
                    Thread.Sleep(1);
                    ReportPortal.Shared.Context.Current.Log.Debug("one more inner qwe " + i);
                }
            }

            Thread.Sleep(1000);

            ReportPortal.Shared.Context.Current.Log.Info("End");
        }

        [Test]
        [Description("Test should fail with timeout")]
        [Timeout(1000)]
        public void Test3()
        {
            System.Threading.Thread.Sleep(3000);
        }

        [Test, Retry(5)]
        [Description("Test should be retried")]
        public void Test4()
        {
            System.Threading.Thread.Sleep(3000);
            ReportPortal.Shared.Context.Current.Log.Trace("class1 test4 log message");
            //throw new Exception("abc");
            Assert.IsTrue(false);
        }

        [Test]
        [Description("Reporting should work even if file attachment was deleted in runtime.")]
        public void AttachmentsAreOptional()
        {
            var tempFile = Path.GetTempFileName();

            TestContext.AddTestAttachment(tempFile);

            File.Delete(tempFile);
        }

        [Test]
        public void TestMultipleAssertions()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, 3, "The first failed assert");
                Assert.AreEqual(4, 5, "The second failed assert");
                throw new Exception("And exception and the end.");
            });
        }

        [Test]
        public void InconclusiveTest()
        {
            Assert.Inconclusive("Why this is inconclusive.");
        }

        [Test]
        public void ManyLogMessages()
        {
            for (int i = 0; i < 20; i++)
            {
                Log4NetLogger.Info($"Log4Net {i}");
            }
        }

        [Test]
        public void ManyLogMessages2()
        {
            for (int i = 0; i < 20; i++)
            {
                ReportPortal.Shared.Context.Current.Log.Message(new LogMessage($"Log {i}") {
                    Level = LogMessageLevel.Info,
                    Time = DateTime.UtcNow
                });
            }
        }

        [Test]
        public void TestWithDynamicAttribute()
        {
            Context.Current.Metadata.Attributes.Add("my_key", "my_value");
        }
    }
}
