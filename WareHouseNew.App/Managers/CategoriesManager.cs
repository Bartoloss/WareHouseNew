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
            

        public void AddCategories(int amountOfCategories)
        {
            for (int i = 1; i <= amountOfCategories; i++)
            {
                Categories category = new Categories();
                category.Id = i;
                Console.WriteLine($"Please enter name of {i} category:");
                string userNameCategory = Console.ReadLine();
                category.CategoryName = userNameCategory;
                _categoriesService.AddItem(category);
            }
        }

        public void GetAllCategories()
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

        public void GetListOfProducts()
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
