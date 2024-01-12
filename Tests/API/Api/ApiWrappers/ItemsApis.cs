using Api.Models;
using Api.TestConfiguration;

namespace Api.ApiWrappers;

public class ItemsApis
{
    private readonly ApiContext _requestContext;

    public ItemsApis(ApiContext requestContext)
    {
        _requestContext = requestContext;
    }

    public async Task<IAPIResponse> GetItems()
    {
        var response = await _requestContext.GetApiRequestContext().GetAsync("/api/items");

        return response;
    }

    public async Task<IAPIResponse> CreateItem(Item item)
    {
        var response = await _requestContext.GetApiRequestContext().PostAsync("/api/items", new APIRequestContextOptions
        {
            DataObject = item
        });

        return response;
    }

    public async Task<IAPIResponse> UpdateItem(Item originalItem, Item newItem)
    {
        var response = await _requestContext.GetApiRequestContext().PutAsync($"/api/items/{originalItem.Id}", new APIRequestContextOptions
        {
            DataObject = newItem
        });

        return response;
    }

    public async Task<IAPIResponse> GetItem(Item item)
    {
        var response = await _requestContext.GetApiRequestContext().GetAsync($"/api/items/{item.Id}");

        return response;
    }
}
