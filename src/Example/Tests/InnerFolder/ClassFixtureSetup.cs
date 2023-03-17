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
            ReportPortal.Shared.Context.Current.Log.Info("OneTimeSetUp message");
            throw new Exception("Assembly SetUpFixture exception.");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ReportPortal.Shared.Context.Current.Log.Info("OneTimeTearDown message");
            throw new Exception("Assembly TearDownFixture exception.");
        }
    }
}
