using ItemE2E.Pages;
using ItemE2E.Services;
using ItemE2E.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace ItemE2E;

public static class TestStartup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        services.AddPlaywright();
        services.AddPagesHandler();
        services.AddConfiguration();
        services.AddPages();
        services.AddScreenshotService();
        services.AddPageDependencyService();
        return services;
    }

    private static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        services.AddSingleton(_ =>
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("Settings/AppSettings.json", false, true)
                .Build();

            return config;
        });

        services.AddOptions<AppSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.Bind(settings);
            });

        return services;
    }

    private static IServiceCollection AddPlaywright(this IServiceCollection services)
    {
        return services.AddScoped<Task<IPage>>(async _ =>
        {
            var playwright = await Playwright.CreateAsync().ConfigureAwait(false);
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 200
            }).ConfigureAwait(false);
            return await browser.NewPageAsync().ConfigureAwait(false);
        });
    }


    private static IServiceCollection AddPages(this IServiceCollection services) =>
        services
            .AddTransient<HomePage>()
            .AddTransient<MainPage>();

    private static IServiceCollection AddScreenshotService(this IServiceCollection services) =>
        services
            .AddScoped<IScreenShotService, ScreenShotService>();


    private static IServiceCollection AddPagesHandler(this IServiceCollection services) =>
        services
            .AddScoped<IPageService, PagesService>();

    private static IServiceCollection AddPageDependencyService(this IServiceCollection services) =>
        services
            .AddScoped<IPageDependencyService, PageDependencyService>();
}
