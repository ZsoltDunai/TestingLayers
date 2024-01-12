using ItemE2E.Pages;

namespace ItemE2E.Services;

public interface IPageService
{
    HomePage HomePage { get; }
    MainPage MainPage { get; }
}

public class PagesService : IPageService
{
    public PagesService(HomePage homePage, MainPage mainPage)
    {
        HomePage = homePage;
        MainPage = mainPage;
    }
    public HomePage HomePage { get; }

    public MainPage MainPage { get; }
}
