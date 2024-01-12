using ItemE2E.Services;


namespace ItemE2E.Pages;

public class MainPage
{
    private readonly IPageDependencyService _pageDependencyService;

    public MainPage(IPageDependencyService pageDependencyService)
    {
        _pageDependencyService = pageDependencyService;
    }

    private ILocator MainPageTitleText => _pageDependencyService.Page.Result.Locator("[data-unique-attribute-title=\"title\"]");

    private ILocator AddNewItemButton => _pageDependencyService.Page.Result.Locator("[data-unique-attribute-new-item=\"AddNewItem\"]");

    private ILocator AddNewItemItemNameTextbox => _pageDependencyService.Page.Result.Locator("[id=\"itemName\"][type=\"text\"]");

    private ILocator CreateItemButton => _pageDependencyService.Page.Result.Locator("[data-unique-attribute-create-item=\"CreateItem\"]");

    private ILocator ItemsListBox => _pageDependencyService.Page.Result.Locator("[data-unique-attribute-section=\"itemsList\"]");

    private ILocator UpdateItemItemNameTextbox => _pageDependencyService.Page.Result.Locator("[id=\"updateItemName\"][type=\"text\"]");

    private ILocator UpdateItemButton => _pageDependencyService.Page.Result.Locator("[data-unique-attribute-update-item=\"UpdateItem\"]");

    public async Task MainPageTitleIsVisible()
    {
        await _pageDependencyService.TakeScreenshotAsync();
        await Assertions.Expect(MainPageTitleText).ToBeVisibleAsync();
    }

    public async Task AddNewItem(string itemName)
    {
        await AddNewItemButton.ClickAsync();
        await AddNewItemItemNameTextbox.FillAsync(itemName);
        await _pageDependencyService.TakeScreenshotAsync();
        await CreateItemButton.ClickAsync();
        await _pageDependencyService.TakeScreenshotAsync();
    }

    public async Task UpdateItem(string originalItemName, string newItemName)
    {
        var items = await GetItems();
        IElementHandle updateButton = null;

        foreach (var item in items)
        {
            if (await item.GetAttributeAsync("data-unique-attribute-item-name") == originalItemName)
            {
                updateButton = await item.QuerySelectorAsync("Button");
                break;
            }
        }

        if (updateButton != null)
        {
            await updateButton.ClickAsync();
            await UpdateItemItemNameTextbox.FillAsync(newItemName);
            await _pageDependencyService.TakeScreenshotAsync();
            await UpdateItemButton.ClickAsync();
            await _pageDependencyService.TakeScreenshotAsync();
        }
    }

    public async Task<IReadOnlyList<IElementHandle>> GetItems()
    {
        var items = await ItemsListBox.ElementHandlesAsync();
        return items;
    }

    public async Task ItemIsVisibleInTheItemsList(string itemToSearch)
    {
        var itemMatches = false;
        
        var items = await GetItems();
        foreach (var item in items)
        {
            var itemName = await item.GetAttributeAsync("data-unique-attribute-item-name");

            if (itemName == itemToSearch)
            {
                itemMatches = true;
                break;
            }
        }

        itemMatches.Should().BeTrue();
        await _pageDependencyService.TakeScreenshotAsync();
    }
}
