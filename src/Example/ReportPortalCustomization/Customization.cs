using NUnit.Engine.Extensibility;
using System;
using System.Threading.Tasks;

namespace Example.ReportPortalCustomization
{
    [Extension]
    public class Customization : NUnit.Engine.ITestEventListener
    {
        public Customization()
        {
            ReportPortal.NUnitExtension.ReportPortalListener.BeforeRunStarted += ReportPortalListener_BeforeRunStarted;

            ReportPortal.NUnitExtension.ReportPortalListener.AfterTestStarted += ReportPortalListener_AfterTestStarted;
            ReportPortal.NUnitExtension.ReportPortalListener.BeforeTestFinished += ReportPortalListener_BeforeTestFinished;
        }

        private void ReportPortalListener_BeforeTestFinished(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemFinishedEventArgs e)
        {
            // don't assign "To investigate" for skipped tests
            if (e.FinishTestItemRequest.Status == ReportPortal.Client.Models.Status.Skipped)
            {
                e.FinishTestItemRequest.Issue = new ReportPortal.Client.Models.Issue
                {
                    Type = ReportPortal.Client.Models.WellKnownIssueType.NotDefect
                };
            }

            // modify description of tests
            var pattern = "{MachineName}";
            if (e.FinishTestItemRequest.Description != null && e.FinishTestItemRequest.Description.Contains(pattern))
            {
                e.FinishTestItemRequest.Description = e.FinishTestItemRequest.Description.Replace(pattern, Environment.MachineName);
            }
        }

        private void ReportPortalListener_AfterTestStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
        {
            e.TestReporter.Log(new ReportPortal.Client.Requests.AddLogItemRequest
            {
                Level = ReportPortal.Client.Models.LogLevel.Trace,
                Time = DateTime.UtcNow,
                Text = "This message is from 'ReportPortalListener_AfterTestStarted' event."
            });

            if (e.StartTestItemRequest.Name.StartsWith("Sync"))
            {
                // waiting until test is being reported to the server and retrieve info
                e.TestReporter.StartTask.Wait();
                var infoTask = Task.Run(async () => await e.Service.GetTestItemAsync(e.TestReporter.TestInfo.Id));
                infoTask.Wait();
                var testInfo = infoTask.Result;
                e.TestReporter.Log(new ReportPortal.Client.Requests.AddLogItemRequest
                {
                    Level = ReportPortal.Client.Models.LogLevel.Trace,
                    Time = DateTime.UtcNow,
                    Text = $"Actual test ID: {testInfo.UniqueId}"
                });
            }

        }

        private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
        {
            // add custom tag
            e.StartLaunchRequest.Tags.Add("custom_tag");

            // change custom description
            e.StartLaunchRequest.Description += Environment.NewLine + Environment.OSVersion;
        }

        public void OnTestEvent(string report)
        {
            
        }
    }
}
