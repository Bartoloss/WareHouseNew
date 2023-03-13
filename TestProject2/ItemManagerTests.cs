using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Concrete;
using WareHouseNew.App.Managers;
using WareHouseNew.Domain.Entity;
using FluentAssertions;

namespace WareHouseNewTests
{
    public class ItemManagerTests
    {

        [Fact]
        public void AddNewItem_AddsProductToTheList()
        {
            //Arrange
            Item product1 = new Item() { Id = 1, Name = "Product1" };
            var mock = new Mock<ItemService>();
            mock.Setup(s => s.AddItem(product1)).Returns(product1.Id);
            var manager = new ItemManager(mock.Object, new MenuActionService(), new CategoriesService(), new CategoriesManager(new CategoriesService()));

            //Act
            manager.AddNewItem();

            //Assert
            mock.Verify(s => s.AddItem(product1));

        }

        
    }
}
