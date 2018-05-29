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
        }

        private void ReportPortalListener_AfterTestStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
        {
            e.TestReporter.Log(new ReportPortal.Client.Requests.AddLogItemRequest
            {
                Level = ReportPortal.Client.Models.LogLevel.Trace,
                Time = DateTime.UtcNow,
                Text = "This message is from 'ReportPortalListener_AfterTestStarted' event."
            });
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
