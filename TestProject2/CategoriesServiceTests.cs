using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Concrete;
using WareHouseNew.Domain.Entity;

namespace WareHouseNewTests
{
    public class CategoriesServiceTests
    {
        [Fact]
        public void AddItem_AddsCategoryToTheList()
        {
            //Arrange
            CategoriesService categoriesService = new CategoriesService();
            Categories category = new Categories() { Id = 1, CategoryName = "Category1" };

            //Act
            int returnedId = categoriesService.AddItem(category);

            //Assert
            Categories? returnedCategory = categoriesService.GetCategoryById(category.Id);
            returnedCategory.Should().NotBeNull();
            returnedCategory.CategoryName.Should().Be(category.CategoryName);
            returnedCategory.CategoryName.Should().NotBeNull();
            returnedId.Should().Be(category.Id);
        }

        [Fact]
        public void RemoveItem_RemoveCategoryFromTheList()
        {
            //Arrange
            CategoriesService categoriesService = new CategoriesService();
            Categories category = new Categories() { Id = 1, CategoryName = "Category1" };
            categoriesService.AddItem(category);

            //Act
            categoriesService.RemoveItem(category);

            //Assert
            Categories result = categoriesService.GetCategoryById(category.Id);
            result.Should().BeNull();
        }

        [Fact]
        public void GetAllItems_ReturnsListOfAllCategories()
        {
            //Arrange
            CategoriesService categoriesService = new CategoriesService();
            Categories category1 = new Categories() { Id = 1, CategoryName = "Category1" };
            Categories category2 = new Categories() { Id = 2, CategoryName = "Category2" };
            categoriesService.AddItem(category1);
            categoriesService.AddItem(category2);

            //Act
            List<Categories> AllCategories = categoriesService.GetAllItems();

            //Assert
            AllCategories.Should().NotBeNull();
            AllCategories.Count.Should().Be(2);
        }

        [Fact]
        public void GetLastId_ReturnsIdOfLastCategory()
        {
            //Arrange
            CategoriesService categoriesService = new CategoriesService();
            Categories category1 = new Categories() { Id = 1, CategoryName = "Category1" };
            Categories category2 = new Categories() { Id = 2, CategoryName = "Category2" };
            categoriesService.AddItem(category1);
            categoriesService.AddItem(category2);

            //Act
            int returnedId = categoriesService.GetLastId();

            //Assert
            returnedId.Should().Be(category2.Id);
        }

        [Fact]
        public void GetCategoryById_ReturnsCategoryOfSelectedId()
        {
            //Arrange
            CategoriesService categoriesService = new CategoriesService();
            Categories category1 = new Categories() { Id = 1, CategoryName = "Category1" };
            categoriesService.AddItem(category1);
            
            //Act
            Categories returnedProduct = categoriesService.GetCategoryById(category1.Id);

            //Assert
            returnedProduct.Should().NotBeNull();
            returnedProduct.Id.Should().Be(category1.Id);
            returnedProduct.CategoryName.Should().Be(category1.CategoryName);

        }

        [Fact]
        public void GetNumberOfAllCategories_ReturnsNumberOfAllCategories()
        {
            //Arrange
            CategoriesService categoriesService = new CategoriesService();
            Categories category1 = new Categories() { Id = 1, CategoryName = "Category1" };
            Categories category2 = new Categories() { Id = 2, CategoryName = "Category2" };
            categoriesService.AddItem(category1);
            categoriesService.AddItem(category2);

            //Act
            int numberOfCategories = categoriesService.GetNumberOfAllCategories();

            //Assert
            numberOfCategories.Should().Be(2);
        }
    }
}
