using NUnit.Framework;
using NUnit.Framework.Interfaces;
using ReportPortal.Shared;
//using LogLevel = ReportPortal.Client.Models.LogLevel;

namespace Example.Tests
{
    [Parallelizable]
    [TestFixture]
    [Description("this is description for Class1")]
    public class Class1
    {
        [Category("T1")]
        [Test]
        public void Test1()
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Trace, "class1 test1 log message");

            var filePath = TestContext.CurrentContext.TestDirectory + "\\dog.png";
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, "my dog {rp#file#" + filePath + "}");
        }

        [Test]
        [Description("this is description for Test2")]
        public void Test2()
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Trace, "class1 test2 log message");
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure) ||
                TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Error) ||
                TestContext.CurrentContext.Result.Outcome.Equals(ResultState.SetUpError) ||
                TestContext.CurrentContext.Result.Outcome.Equals(ResultState.SetUpFailure))
            {
                string base64 = GetScreenshotAsBase64EncodedString();

                if (base64 != null)
                {
                    Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Error, "Screen shot on failure {rp#base64#image/png#" + base64 + "}");
                }
            }
        }

        private string GetScreenshotAsBase64EncodedString()
        {
            // assume that screenshot is taken with Selenium WebDriver with commented code.
            // where WebDriver.Instance is an instanse of IWebDriver

            // ((ITakesScreenshot) WebDriver.Instance).GetScreenshot().AsBase64EncodedString;
            return null;
        }
    }
}
