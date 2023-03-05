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
            string userNameCategory = Console.ReadLine();
            category.CategoryName = userNameCategory;
            _categoriesService.AddItem(category);

        }
        
        public void ViewAllCategories()
        {
            var categories = _categoriesService.GetAllItems();
            if (categories.Any())
            {
                foreach (var category in categories)
                {
                    Console.WriteLine($"{category.Id}.{category.CategoryName}");
                }
            }
        }

        public void ViewListOfCategories()
        {
            var categories = _categoriesService.GetAllItems();
            if (categories.Any())
            {
                Console.WriteLine("Please select category of products to display:");
                foreach (var category in categories)
                {
                    Console.WriteLine($"{category.Id}.List of {category.CategoryName} products.");
                }
                
            }
        }
    }
}
