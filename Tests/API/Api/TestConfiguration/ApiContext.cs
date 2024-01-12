using Api.Settings;
using Microsoft.Extensions.Options;

namespace Api.TestConfiguration;

public class ApiContext : IDisposable
{
    private IAPIRequestContext _request;
    private readonly IPlaywright _playwright;
    private readonly string _apiUrl;

    public ApiContext(IOptions<AppSettings> appSettingsOptions)
    {
        _apiUrl = appSettingsOptions.Value.ApiUrl;

        _playwright = Playwright.CreateAsync().GetAwaiter().GetResult();

        _request = _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = _apiUrl,
            IgnoreHTTPSErrors = true
        }).GetAwaiter().GetResult();
    }

    public IAPIRequestContext GetApiRequestContext()
    {
        return _request;
    }

    public void Dispose()
    {
        _playwright.Dispose();
    }
}
