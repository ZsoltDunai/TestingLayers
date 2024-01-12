using ItemE2E.Settings;
using Microsoft.Extensions.Options;

namespace ItemE2E.Services;

public interface IPageDependencyService
{
    Task<IPage> Page { get; }

    IOptions<AppSettings> AppSettings { get; }

    IScreenShotService ScreenShotService { get; }

    ScenarioContext ScenarioContext { get; }

    Task TakeScreenshotAsync();
}

public class PageDependencyService : IPageDependencyService, IDisposable
{
    public PageDependencyService(Task<IPage> page, IOptions<AppSettings> appSettings, IScreenShotService screenShotService, ScenarioContext scenarioContext)
    {
        Page = page;
        Page.Result.SetDefaultTimeout(5000);
        AppSettings = appSettings;
        ScreenShotService = screenShotService;
        ScenarioContext = scenarioContext;
    }

    public void Dispose()
    {
        Page.Result.Context.Browser?.CloseAsync().ConfigureAwait(false);
    }

    public Task<IPage> Page { get; }
    public IOptions<AppSettings> AppSettings { get; }
    public IScreenShotService ScreenShotService { get; }
    public ScenarioContext ScenarioContext { get; }

    public async Task TakeScreenshotAsync()
    {
        await ScreenShotService.TakeScreenshot(ScenarioContext.ScenarioInfo.Title, ScenarioContext.StepContext.StepInfo.Text ?? null);
    }
}
