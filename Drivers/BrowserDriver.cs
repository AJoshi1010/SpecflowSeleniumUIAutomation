using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow.Infrastructure;

namespace tlfwebautomation.Drivers
{
    public class BrowserDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        private bool _isDisposed;

        public BrowserDriver(ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
            _specFlowOutputHelper = specFlowOutputHelper;

        }

        public IWebDriver Current => _currentWebDriverLazy.Value;
        /// <summary>
        /// Opening a chrome browser
        /// </summary>
        private IWebDriver CreateWebDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            var chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
            _specFlowOutputHelper.WriteLine("Browser session active");
            return chromeDriver;
        }

        /// <summary>
        /// Closing the browser
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
                _specFlowOutputHelper.WriteLine("Browser session closed");
            }
            _isDisposed = true;
        }
    }
}
