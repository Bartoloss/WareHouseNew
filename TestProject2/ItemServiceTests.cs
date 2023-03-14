using WareHouseNew.App;
using WareHouseNew.App.Concrete;
using WareHouseNew.Domain;
using WareHouseNew.Domain.Entity;
using FluentAssertions;
using System.Xml.Linq;

namespace WareHouseNewTests
{
    public class ItemServiceTests
    {
        [Fact]
        public void AddItem_AddsProductToTheList()
        {
            //Arrange
            ItemService itemService = new ItemService();
            Item product = new Item() { Id = 1, Name = "Product1" };

            //Act
            int returnedId = itemService.AddItem(product);

            //Assert
            Item returnedProduct = itemService.GetItemById(product.Id);
            returnedProduct.Should().NotBeNull();
            returnedProduct.Name.Should().Be(product.Name);
            returnedProduct.Name.Should().NotBeNull();
            returnedId.Should().Be(product.Id);
        }

        [Fact]
        public void RemoveItem_RemoveProductFromTheList()
        {

            //Arrange
            ItemService itemService = new ItemService();
            Item product = new Item() { Id = 1, Name = "Product1" };
            itemService.AddItem(product);

            //Act
            itemService.RemoveItem(product);

            //Assert
            Item result = itemService.GetItemById(product.Id);
            result.Should().BeNull();
        }

        [Fact]
        public void GetAllItems_ReturnsListOfAllProducts()
        {
            //Arrange
            ItemService itemService = new ItemService();
            Item product1 = new Item() { Id = 1, Name = "Product1" };
            Item product2 = new Item() { Id = 2, Name = "Product2" };
            itemService.AddItem(product1);
            itemService.AddItem(product2);

            //Act
            List<Item> AllItems = itemService.GetAllItems();

            //Assert
            AllItems.Should().NotBeNull();
            AllItems.Count.Should().Be(2);
        }

        [Fact]
        public void GetLastId_ReturnsIdOfLastProduct()
        {
            //Arrange
            ItemService itemService = new ItemService();
            Item product1 = new Item() { Id = 1, Name = "Product1" };
            Item product2 = new Item() { Id = 2, Name = "Product2" };
            itemService.AddItem(product1);
            itemService.AddItem(product2);

            //Act
            int returnedId = itemService.GetLastId();

            //Assert
            returnedId.Should().Be(product2.Id);
        }

        [Fact]
        public void GetItemById_ReturnsProductOfSelectedId()
        {
            //Arrange
            ItemService itemService = new ItemService();
            Item product = new Item() { Id = 1, Name = "Product1" };
            itemService.AddItem(product);

            //Act
            Item returnedProduct = itemService.GetItemById(product.Id);

            //Assert
            returnedProduct.Should().NotBeNull();
            returnedProduct.Id.Should().Be(product.Id);
            returnedProduct.Name.Should().Be(product.Name);

        }

        [Fact]
        public void GetItemsByCategory_ReturnsProductsByCategory()
        {
            //Arrange
            ItemService itemService = new ItemService();
            Item product1 = new Item() { Id = 1, Name = "Product1", CategoryId = 1 };
            Item product2 = new Item() { Id = 2, Name = "Product2", CategoryId = 1 };
            Item product3 = new Item() { Id = 3, Name = "Product3", CategoryId = 2 };
            itemService.AddItem(product1);
            itemService.AddItem(product2);
            itemService.AddItem(product3);

            //Act
            List<Item> itemsByCategory = itemService.GetItemsByCategory(1);

            //Assert
            itemsByCategory.Should().NotBeNull();
            itemsByCategory.Count.Should().Be(2);

        }

        [Fact]
        public void GetItemsWithLowStack_ReturnsProductsWithLowStack()
        {
            //Arrange
            ItemService itemService = new ItemService();
            Item product1 = new Item() { Id = 1, Name = "Product1", Amount = 5 };
            Item product2 = new Item() { Id = 2, Name = "Product2", Amount = 10 };
            Item product3 = new Item() { Id = 3, Name = "Product3", Amount = 15 };
            itemService.AddItem(product1);
            itemService.AddItem(product2);
            itemService.AddItem(product3);

            //Act
            List<Item> productsWithLowStack = itemService.GetItemsWithLowStack();

            //Assert
            productsWithLowStack.Should().NotBeNull();
            productsWithLowStack.Count.Should().Be(2);

        }

    }
}