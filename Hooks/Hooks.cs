using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Bits.Api.Tests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var htmlReporter = new ExtentHtmlReporter(Directory.GetCurrentDirectory());
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Config.ReportName = "API Test Summary Report";
            htmlReporter.Start();

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext sc)
        {
            scenario = extent.CreateTest(sc.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterStep(ScenarioContext sc)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            
            if (sc.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepType +" " + ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text);
            }
            if (sc.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "When")
                    scenario.CreateNode<When>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "Then")
                    scenario.CreateNode<Then>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "And")
                    scenario.CreateNode<And>(stepType + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
            }
        }
        
        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }

    }
}
