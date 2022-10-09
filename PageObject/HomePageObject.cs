using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class HomePageObject
{
    private const string TLFUrl = "https://tfl.gov.uk";
    private readonly IWebDriver _webDriver;
    public const int MaxtWaitInSeconds = 4;

    public HomePageObject(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    //Finding elements
    private IWebElement FromElement => _webDriver.FindElement(By.Id("InputFrom"));
    private IWebElement ToElement => _webDriver.FindElement(By.Id("InputTo"));
    private IWebElement JourneyPlannerButtonElement => _webDriver.FindElement(By.Id("plan-journey-button"));
    private IWebElement EditJourneyLinkElement => _webDriver.FindElement(By.LinkText("Edit journey"));
    private IWebElement ClearFromInputElement => _webDriver.FindElement(By.LinkText("Clear From location"));
    private IWebElement ClearToInputElement => _webDriver.FindElement(By.LinkText("Clear To location"));
    private IWebElement PlanAJourneyLinkElement => _webDriver.FindElement(By.LinkText("Plan a journey"));
    private IWebElement RecentsLinkElement => _webDriver.FindElement(By.LinkText("Recents"));
    private IWebElement ResultElement => _webDriver.FindElement(By.Id("result"));
    private IWebElement ResetButtonElement => _webDriver.FindElement(By.Id("reset-button"));
    private string FromOrToDropdown = "//strong[contains(text(), '%s')]/parent::span";

    // Interracting with the elements using modular methods
    public void EnterFrom(string FromLocation)
    {
        FromElement.Click();
        FromElement.SendKeys(FromLocation);
        ReadOnlyCollection<IWebElement> DropdownMenu = WaitForElementsPresence(By.XPath(FromOrToDropdown.Replace("%s", FromLocation)));
        if (DropdownMenu != null)
        {
            DropdownMenu[0].Click();
        }
    }

    public void EnterTo(string ToLocation)
    {
        ToElement.Click();
        ToElement.SendKeys(ToLocation);
        ReadOnlyCollection<IWebElement> DropdownMenu = WaitForElementsPresence(By.XPath(FromOrToDropdown.Replace("%s", ToLocation)));
        if (DropdownMenu != null)
        {
            DropdownMenu[0].Click();
        }
    }

    public void ClickOnPlanJourney()
    {
        JourneyPlannerButtonElement.Click();
    }

    public void EnsureTLFPageIsOpenAndLoaded()
    {
        if (_webDriver.Url != TLFUrl)
        {
            _webDriver.Url = TLFUrl;
        }
        AcceptCookies();
        WaitForElementPresence(FromElement);
    }

    public void ClearFromInput()
    {
        FromElement.Clear();
    }
    public void ClearToInput()
    {
        ToElement.Clear();
    }

    public string GetText(string errorText)
    {
        return WaitForElementPresence(GetXpathFromText(errorText)).GetAttribute("textContent");
    }

    public string GetSearchResultText(string location)
    {
        string xpathText = "//strong[contains(text(),'%s')]/parent::span[contains(text(),'We found more than one location matching')]".Replace("%s", location);
        return WaitForElementPresence(By.XPath(xpathText)).GetAttribute("textContent");
    }

    public void ClickOnEditJourney()
    {
        EditJourneyLinkElement.Click();
    }

    public void ClearAndEnterFromLocation(string FromLocation)
    {
        WaitForElementPresence(ClearFromInputElement).Click();
        FromElement.SendKeys(FromLocation);
        ReadOnlyCollection<IWebElement> DropdownMenu = WaitForElementsPresence(By.XPath(FromOrToDropdown.Replace("%s", FromLocation)));
        if (DropdownMenu != null)
        {
            DropdownMenu[0].Click();
        }
    }

    public void ClearAndEnterToLocation(string ToLocation)
    {
        WaitForElementPresence(ClearToInputElement).Click();
        ToElement.SendKeys(ToLocation);
        ReadOnlyCollection<IWebElement> DropdownMenu = WaitForElementsPresence(By.XPath(FromOrToDropdown.Replace("%s", ToLocation)));
        if (DropdownMenu != null)
        {
            DropdownMenu[0].Click();
        }
    }

    public void ViewRecentJourneysTab()
    {
        PlanAJourneyLinkElement.Click();
        WaitForElementPresence(RecentsLinkElement).Click();
    }

    /// <summary>
    /// Helper method to accept the cookies on the UI for TFL website
    /// </summary>
    private void AcceptCookies()
    {
        DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(_webDriver);
        fluentWait.Timeout = TimeSpan.FromSeconds(MaxtWaitInSeconds);
        fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        fluentWait.Message = "Element to be searched not found";
        fluentWait.Until(x => x.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"))).Click();
        fluentWait.Until(x => x.FindElement(By.XPath("//div[@id=\"cb-confirmedSettings\"]/div[@id=\"cb-buttons\"]/button"))).Click();
    }

    /// <summary>
    /// Helper method to wait for element presence
    /// <param name="by">By element identifier</param>
    /// </summary>
    private IWebElement WaitForElementPresence(By by)
    {
        DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(_webDriver);
        fluentWait.Timeout = TimeSpan.FromSeconds(MaxtWaitInSeconds);
        fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        fluentWait.Message = "Element not found on TLF page";
        return fluentWait.Until(x => x.FindElement(by));
    }

    /// <summary>
    /// Helper method to wait for presence in case of multiple elements
    /// <param name="by">By element identifier</param>
    /// </summary>
    private ReadOnlyCollection<IWebElement> WaitForElementsPresence(By by)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(MaxtWaitInSeconds));
            return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);

        }
        catch (WebDriverException)
        {
            return null;
        }
    }

    /// <summary>
    /// Helper method to wait for element presence
    /// <param name="element">Identified element for which the wait is needed</param>
    /// </summary>
    private IWebElement WaitForElementPresence(IWebElement element)
    {
        DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(_webDriver);
        fluentWait.Timeout = TimeSpan.FromSeconds(MaxtWaitInSeconds);
        fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        fluentWait.Message = "Element not found on TLF page";
        return fluentWait.Until(x => element);
    }

    /// <summary>
    /// Helper method to get XPath using external string
    /// <param name="text">External string required n XPath</param>
    /// </summary>
    private By GetXpathFromText(string text)
    {
        string xpathText = "//*[contains(text(),\"%s\")]".Replace("%s", text);
        return By.XPath(xpathText);
    }
}