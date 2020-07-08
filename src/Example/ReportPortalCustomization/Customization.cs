using NUnit.Engine.Extensibility;
using ReportPortal.Client.Abstractions.Models;
using ReportPortal.Client.Abstractions.Requests;
using ReportPortal.Client.Abstractions.Responses;
using System;
using System.Linq;
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
            if (e.FinishTestItemRequest.Status == Status.Skipped)
            {
                e.FinishTestItemRequest.Issue = new Issue
                {
                    Type = WellKnownIssueType.NotDefect
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
            e.TestReporter.Log(new CreateLogItemRequest
            {
                Level = LogLevel.Trace,
                Time = DateTime.UtcNow,
                Text = "This message is from 'ReportPortalListener_AfterTestStarted' event."
            });

            if (e.StartTestItemRequest.Name.StartsWith("Sync"))
            {
                // waiting until test is being reported to the server and retrieve info
                e.TestReporter.StartTask.Wait();
                var infoTask = Task.Run(async () => await e.Service.TestItem.GetAsync(e.TestReporter.Info.Uuid));
                infoTask.Wait();
                var testInfo = infoTask.Result;
                e.TestReporter.Log(new CreateLogItemRequest
                {
                    Level = LogLevel.Trace,
                    Time = DateTime.UtcNow,
                    Text = $"Actual test ID: {testInfo.UniqueId}"
                });
            }

        }

        private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
        {
            // add custom attributes
            e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Value = "custom_tag" });

            // change custom description
            e.StartLaunchRequest.Description += Environment.NewLine + Environment.OSVersion;
        }

        public void OnTestEvent(string report)
        {

        }
    }
}
