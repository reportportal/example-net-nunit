using NUnit.Framework;
using System;

namespace Example.Tests.InnerFolder
{
    [SetUpFixture]
    public class ClassFixtureSetup
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ReportPortal.Shared.Log.Info("OneTimeSetUp message");
            throw new Exception("Assembly SetUpFixture exception.");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ReportPortal.Shared.Log.Info("OneTimeTearDown message");
            throw new Exception("Assembly TearDownFixture exception.");
        }
    }
}
