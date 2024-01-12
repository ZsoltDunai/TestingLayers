using Api.Services;
using Api.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api.TestConfiguration;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Self)]
public class TestBase
{
    private static ServiceProvider ServiceProvider { get; set; } = null!;
    private IServiceScope ServiceScope { get; set; } = null!;
    public ITestMethods TestMethods { get; private set; } = null!;
    public ApiContext ApiContext { get; private set; } = null!;
    public IOptions<AppSettings> AppSettingsOptions { get; private set; } = null!;

    [OneTimeSetUp]
    public static Task OneTimeSetupAsync()
    {
        var startUp = new TestStartup();
        var services = startUp.CreateServices();

        ServiceProvider = services.BuildServiceProvider() ?? throw new Exception("Services is null!");

        return Task.CompletedTask;
    }

    [SetUp]
    public virtual Task SetupAsync()
    {
        ServiceScope = ServiceProvider.CreateScope();
        var scopedServiceProvider = ServiceScope.ServiceProvider;

        var testMethods = scopedServiceProvider.GetRequiredService<ITestMethods>();
        var apiContext = scopedServiceProvider.GetRequiredService<ApiContext>();
        var appSettings = scopedServiceProvider.GetRequiredService<IOptions<AppSettings>>();

        TestMethods = testMethods;
        ApiContext = apiContext;
        AppSettingsOptions = appSettings;

        return Task.CompletedTask;
    }

    [TearDown]
    public Task TearDownAsync()
    {
        ServiceScope.Dispose();
        return Task.CompletedTask;
    }
}
