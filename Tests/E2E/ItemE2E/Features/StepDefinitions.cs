using ItemE2E.Extensions;
using ItemE2E.Services;

namespace ItemE2E.Features
{
    [Binding]
    public class StepDefinitions
    {
        private readonly IPageService _pageService;

        public StepDefinitions(IPageService pageService)
        {
            _pageService = pageService;
        }

        [When(@"I navigate to item page")]
        public async Task WhenINavigateToItemPageAsync()
        {
            await _pageService.HomePage.GoToItemsHomepage();
        }

        [When(@"the following user tries to login")]
        public async Task WhenTheFollowingUserTriesToLoginAsync(Table table)
        {
            var tableValues = table.ToDictionary();
            tableValues.TryGetValue("Username", out var username);
            tableValues.TryGetValue("Password", out var password);
            await _pageService.HomePage.Login(username, password);
        }

        [Then(@"the main page is visible")]
        public async Task ThenTheMainPageIsVisibleAsync()
        {
            await _pageService.MainPage.MainPageTitleIsVisible();
        }

        [When(@"the following item is added")]
        public async Task WhenTheFollowingItemIsAddedAsync(Table table)
        {
            var tableValues = table.ToDictionary();
            await _pageService.MainPage.AddNewItem(tableValues["ItemName"]);
        }

        [When(@"the following item is updated")]
        public async Task WhenTheFollowingItemIsUpdatedAsync(Table table)
        {
            var tableValues = table.ToDictionary();
            await _pageService.MainPage.UpdateItem(tableValues["OriginalItemName"], tableValues["NewItemName"]);
        }

        [Then(@"the following item is visible in the items list")]
        public async Task ThenTheFollowingItemIsVisibleInTheItemsListAsync(Table table)
        {
            var tableValues = table.ToDictionary();
            await _pageService.MainPage.ItemIsVisibleInTheItemsList(tableValues["ItemName"]);
        }
    }
}
