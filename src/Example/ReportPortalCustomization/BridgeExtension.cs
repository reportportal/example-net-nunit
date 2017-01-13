namespace Example.ReportPortalCustomization
{
    public class BridgeExtension : ReportPortal.Shared.IBridgeExtension
    {
        public void Log(ReportPortal.Client.Models.LogLevel level, string message)
        {
            NUnit.Framework.TestContext.Progress.WriteLine(message);
        }
    }
}
