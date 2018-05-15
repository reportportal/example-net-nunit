using NUnit.Engine.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.ReportPortalCustomization
{
    [Extension]
    public class Customization : NUnit.Engine.ITestEventListener
    {
        public Customization()
        {
            ReportPortal.NUnitExtension.ReportPortalListener.BeforeRunStarted += ReportPortalListener_BeforeRunStarted;
            ReportPortal.NUnitExtension.ReportPortalListener.BeforeSuiteStarted += ReportPortalListener_BeforeSuiteStarted;
            ReportPortal.NUnitExtension.ReportPortalListener.BeforeTestStarted += ReportPortalListener_BeforeTestStarted;
        }

        private void ReportPortalListener_BeforeSuiteStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
        {
            e.Canceled = ShouldBeCancelled(e.TestItem.Name);
            e.TestItem.Name = AdjustTestName(e.TestItem.Name);
        }

        private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
        {
            // You can add some tag to a launch. For example, browser name that allow you to create a launch filter by browsers
            e.Launch.Tags.Add("Chrome");

            // You can add some additional information to a launch description. For example, environment parameters or a link to the test application
            e.Launch.Description += Environment.NewLine + Environment.OSVersion;
        }

        private void ReportPortalListener_BeforeTestStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
        {
            e.TestItem.Name = AdjustTestName(e.TestItem.Name);
        }

        /// Namespace is split by a dot, each such part is a nest in the report.
        /// Here is the initial nesting if you do not customize it:
        /// 
        /// Example.dll
        /// |-Example
        ///     |-Tests
        ///         |-Class1
        ///             |-Test1
        ///             |-Test2
        ///         |-Class2
        ///             |-Test1
        ///             |-Test2
        ///             |-Test3
        /// 
        /// Here is the nesting after customization:
        /// Tests
        ///    |-Class1
        ///        |-Test1
        ///        |-Test2
        ///    |-Class2
        ///        |-Test1
        ///        |-Test2
        ///        |-Test3

        /// <summary>
        /// Get rid of redundant nesting
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool ShouldBeCancelled(string name)
        {
            List<string> items = new List<string>
            {
                "Example.dll",  // assembly name
                "Example"       // part of namespace 
            };

            return items.Any(item => item == name);
        }

        /// <summary>
        /// Make test name more attractive
        /// </summary>
        /// <param name="name">test name</param>
        /// <returns></returns>
        private string AdjustTestName(string name)
        {
            name = SplitWithSpaceByCapitalLetters(name);
            name = name.Replace('_', ' ');
            return name;
        }

        private string SplitWithSpaceByCapitalLetters(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
                builder.Append(c);
            }

            return builder.ToString();
        }

        public void OnTestEvent(string report)
        {
            
        }
    }
}
