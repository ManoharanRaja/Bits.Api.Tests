using TechTalk.SpecFlow;

namespace Bits.Api.Tests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext; 
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
             
        }

        [BeforeScenario]
        public void BeforeScenario()
        {

        }

        [BeforeStep]
        public void BeforeStep()
        {

        }

        [AfterStep]
        public void AfterStep()
        {

        }

        [AfterScenario]
        public void AfterScenario()
        {

        }
        
        [AfterTestRun]
        public static void AfterTestRun()
        {

        }

    }
}
