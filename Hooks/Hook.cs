using System.IO;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;
using tlfwebautomation.Drivers;

namespace tlfwebautomation.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        private readonly BrowserDriver _browserDriver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        ///<summary>
        ///  Load TLF website before each scenario tagged with "TLF"
        ///</summary>
        [BeforeScenario("TLF")]
        public static void BeforeScenario(BrowserDriver browserDriver)
        {
            var homePageObject = new HomePageObject(browserDriver.Current);
            homePageObject.EnsureTLFPageIsOpenAndLoaded();
        }

    }
}
