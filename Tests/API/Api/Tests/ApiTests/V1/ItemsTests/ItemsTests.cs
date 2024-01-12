using Api.Models;
using Api.TestConfiguration;
using FluentAssertions;
using System.Net;

namespace Api.Tests.ApiTests.V1.ItemsTests;

[TestFixture]
public class ItemsTests : TestBase
{
    [Test]
    public async Task GetItemsSuccesfully()
    {
        //Act
        var response = await TestMethods.ItemsApis.GetItems();

        var responseObject = await response.DeserializeResponseAsync<List<Item>>();

        //Assert
        responseObject.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task GetItemByIdReturnNotFound()
    {
        //Arrange
        var item = new Item
        {
            Id = 123,
            Name = "Item 123",
        };

        //Act
        var response = await TestMethods.ItemsApis.GetItem(item);

        //Assert
        response.Status.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Test]
    public async Task CreateItem()
    {
        //Arrange
        var item = new Item
        {
            Id = 13,
            Name = "Item 13",
        };

        //Act
        var response = await TestMethods.ItemsApis.CreateItem(item);

        var responseObject = await response.DeserializeResponseAsync<Item>();

        //Assert
        responseObject.Should().NotBeNull();
        responseObject.Id.Should().Be(item.Id);
        responseObject.Name.Should().Be(item.Name);
    }

    [Test]
    public async Task UpdateItem()
    {
        //Arrange
        var originalItem = new Item
        {
            Id = 1,
            Name = "Item 1",
        };

        var newItem = new Item
        {
            Id = 22,
            Name = "Item 22",
        };

        //Act
        var response = await TestMethods.ItemsApis.UpdateItem(originalItem, newItem);

        //Assert
        response.Status.Should().Be((int)HttpStatusCode.NoContent);
    }
}
