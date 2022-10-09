using TechTalk.SpecFlow;
using tlfwebautomation.Drivers;
using FluentAssertions;

namespace tlfwebautomation.Steps
{
    [Binding]
    public sealed class JourneyPlannerStepDefinitions
    {

        private readonly HomePageObject _homePageObject;

        public JourneyPlannerStepDefinitions(BrowserDriver browserDriver)
        {
            _homePageObject = new HomePageObject(browserDriver.Current);
        }

        [Given("the from location is (.*)")]
        public void GivenTheFromLocationIs(string fromLocation)
        {
            _homePageObject.EnterFrom(fromLocation);
        }

        [Given("the to location is (.*)")]
        public void GivenTheToLocationIs(string toLocation)
        {
            _homePageObject.EnterTo(toLocation);
        }

        [When("the journey is planned")]
        public void WhenTheJourneyIsPlanned()
        {
            _homePageObject.ClickOnPlanJourney();
        }

        [Then("a message '(.*)' should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed(string ExpectedResult)
        {
            string actualResult = _homePageObject.GetText(ExpectedResult).TrimEnd();
            actualResult.Should().Be(ExpectedResult);
        }

        [Given("the locations are not provided")]
        public void GivenTheLocationsAreNotProvided()
        {
            _homePageObject.ClearFromInput();
            _homePageObject.ClearToInput();
        }

        [When("an edit is made on current planned journey")]
        public void WhenAnEditIsMadeOnCurrentPlannedJourney()
        {
            _homePageObject.ClickOnEditJourney();
        }

        [Given("clear and update new from location as (.*)")]
        public void GivenClearAndUpdateNewFromLocationAs(string fromLocation)
        {
            _homePageObject.ClearAndEnterFromLocation(fromLocation);
        }

        [Given("clear and update new to location as (.*)")]
        public void GivenClearAndUpdateNewToLocationAs(string toLocation)
        {
            _homePageObject.ClearAndEnterToLocation(toLocation);
        }

        [When("all recently planned journeys are viewed")]
        public void WhenAllRecentlyPlannedJourneysAreViewed()
        {
            _homePageObject.ViewRecentJourneysTab();
        }
    }
}
