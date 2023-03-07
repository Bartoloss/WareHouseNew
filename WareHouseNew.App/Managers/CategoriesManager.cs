using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Concrete;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Managers
{
    public class CategoriesManager
    {
        public CategoriesService _categoriesService;

        public CategoriesManager(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public void AddCategory(int categoryId)
        {
            Categories category = new Categories();
            category.Id = categoryId;
            Console.WriteLine($"Please enter name of {categoryId} category:");
            string userNameOfAddedCategory = Console.ReadLine();
            category.CategoryName = userNameOfAddedCategory;
            _categoriesService.AddItem(category);

        }
        
        public void ViewAllCategories()
        {
            var allCategories = _categoriesService.GetAllItems();
            if (allCategories.Any())
            {
                foreach (var category in allCategories)
                {
                    Console.WriteLine($"{category.Id}.{category.CategoryName}");
                }
            }
        }

        public void ViewListOfCategories()
        {
            var ListofAllCategories = _categoriesService.GetAllItems();
            if (ListofAllCategories.Any())
            {
                Console.WriteLine("Please select category of products to display:");
                foreach (var category in ListofAllCategories)
                {
                    Console.WriteLine($"{category.Id}.List of {category.CategoryName} products.");
                }
                
            }
        }
    }
}
