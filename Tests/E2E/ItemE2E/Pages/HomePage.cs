using ItemE2E.Services;

namespace ItemE2E.Pages;

public class HomePage
{
    private readonly IPageDependencyService _pageDependencyService;

    public HomePage(IPageDependencyService pageDependencyService)
    {
        _pageDependencyService = pageDependencyService;
    }

    private ILocator UsernameTextbox => _pageDependencyService.Page.Result.Locator("[id=\"username\"]");

    private ILocator PasswordTextbox => _pageDependencyService.Page.Result.Locator("[id=\"password\"]");

    private ILocator LoginButton => _pageDependencyService.Page.Result.Locator("button[type=\"submit\"]");

    public async Task Login(string userName, string password)
    {
        await UsernameTextbox.FillAsync(userName);
        await PasswordTextbox.FillAsync(password);
        await _pageDependencyService.TakeScreenshotAsync();
        await LoginButton.ClickAsync();
    }

    public async Task GoToItemsHomepage()
    {
        await _pageDependencyService.Page.Result.GotoAsync(_pageDependencyService.AppSettings.Value.UiUrl);
        await _pageDependencyService.TakeScreenshotAsync();
    }
}
