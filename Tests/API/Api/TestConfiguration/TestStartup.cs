using Api.ApiWrappers;
using Api.Services;
using Api.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.TestConfiguration;

public class TestStartup
{
    public IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        AddApiContext(services);
        AddTestMethodsInterface(services);
        AddApiWrappers(services);
        AddConfiguration(services);
        return services;
    }

    private IServiceCollection AddTestMethodsInterface(IServiceCollection services) =>
    services
            .AddScoped<ITestMethods, TestMethodsService>();

    private IServiceCollection AddApiWrappers(IServiceCollection services) =>
        services
            .AddTransient<ItemsApis>();

    private IServiceCollection AddApiContext(IServiceCollection services) =>
        services
            .AddScoped<ApiContext>();

    private static IServiceCollection AddConfiguration(IServiceCollection services)
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
}
