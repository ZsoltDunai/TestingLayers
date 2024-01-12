using System.Text.Json;

namespace Api;

public static class ExtensionMethods
{
    /// <summary>
    /// Check if response is okay and deserializes it.
    /// </summary>
    /// <param name="response"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T> DeserializeResponseAsync<T>(this IAPIResponse response)
    {
        await Assertions.Expect(response).ToBeOKAsync();

        var json = await response.TextAsync();

        return DeserializeFromCamelCase<T>(json);
    }

    public static T DeserializeFromCamelCase<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }) ?? throw new Exception("Json is null!");
    }
}
