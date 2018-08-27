using NUnit.Engine.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.ReportPortalCustomization
{
    [Extension]
    public class Customization : NUnit.Engine.ITestEventListener
    {
        public Customization()
        {
            ReportPortal.NUnitExtension.ReportPortalListener.BeforeRunStarted += ReportPortalListener_BeforeRunStarted;

            ReportPortal.NUnitExtension.ReportPortalListener.BeforeSuiteStarted += ReportPortalListener_BeforeSuiteStarted;

            ReportPortal.NUnitExtension.ReportPortalListener.AfterTestStarted += ReportPortalListener_AfterTestStarted;
            ReportPortal.NUnitExtension.ReportPortalListener.BeforeTestFinished += ReportPortalListener_BeforeTestFinished;
        }

        private void ReportPortalListener_BeforeTestFinished(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemFinishedEventArgs e)
        {
            // don't assign "To investigate" for skipped tests
            if (e.TestItem.Status == ReportPortal.Client.Models.Status.Skipped)
            {
                e.TestItem.Issue = new ReportPortal.Client.Models.Issue
                {
                    Type = ReportPortal.Client.Models.WellKnownIssueType.NotDefect
                };
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

            if (e.TestItem.Name.StartsWith("Sync"))
            {
                // waiting until test is being reported to the server and retrieve info
                e.TestReporter.StartTask.Wait();
                var infoTask = Task.Run(async () => await e.Service.GetTestItemAsync(e.TestReporter.TestId));
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

        private void ReportPortalListener_BeforeSuiteStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
        {
            if (e.TestItem.Name == "Example.dll")
            {
                e.Canceled = true;
            }
        }

        private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
        {
            // add custom tag
            e.Launch.Tags.Add("custom_tag");

            // change custom description
            e.Launch.Description += Environment.NewLine + Environment.OSVersion;
        }

        public void OnTestEvent(string report)
        {
            
        }
    }
}
