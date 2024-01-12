using Api.ApiWrappers;

namespace Api.Services;

public interface ITestMethods
{
    ItemsApis ItemsApis { get; }
}

public class TestMethodsService : ITestMethods
{
    public TestMethodsService(ItemsApis itemsApis)
    {
        ItemsApis = itemsApis;
    }
    public ItemsApis ItemsApis { get; }
}
