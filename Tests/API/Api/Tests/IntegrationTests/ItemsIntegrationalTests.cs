using Api.Models;
using Api.TestConfiguration;
using FluentAssertions;
using System.Net;

namespace Api.Tests.IntegrationTests;


[TestFixture]
public class ItemsIntegrationalTests : TestBase
{
    [Test]
    public async Task UpdateItemIntegration()
    {
        //Arrange
        Random random = new Random();
        var getItemsResponse = await TestMethods.ItemsApis.GetItems();
        var getItemsObject = await getItemsResponse.DeserializeResponseAsync<List<Item>>();

        var selectedItemIndex = random.Next(getItemsObject.Count);

        var selectedItem = getItemsObject.ElementAt(selectedItemIndex);

        var getItemDetailsResponse = await TestMethods.ItemsApis.GetItem(selectedItem);
        var getItemDetailsObject = await getItemDetailsResponse.DeserializeResponseAsync<Item>();

        var newItem = new Item
        {
            Id = 99,
            Name = "Item 99",
        };

        //Act
        var putItemResponse = await TestMethods.ItemsApis.UpdateItem(getItemDetailsObject, newItem);

        //Assert
        putItemResponse.Status.Should().Be((int)HttpStatusCode.NoContent);
    }
}
