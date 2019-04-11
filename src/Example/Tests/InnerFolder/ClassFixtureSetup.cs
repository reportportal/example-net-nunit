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
    [SetUpFixture]
    public class ClassFixtureSetup
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, "OneTimeSetUp message");
            throw new Exception("Assembly SetUpFixture exception.");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, "OneTimeTearDown message");
            throw new Exception("Assembly TearDownFixture exception.");
        }
    }
}
