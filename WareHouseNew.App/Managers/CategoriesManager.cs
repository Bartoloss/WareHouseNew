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

        public CategoriesManager()
        {
        }

        public CategoriesManager(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public void AddCategory()
        {
            Categories category = new Categories();
            int categoryId = _categoriesService.GetLastId() + 1;
            string userNameOfAddedCategory;
            
            Console.WriteLine($"Please enter name of {categoryId} category:");
            userNameOfAddedCategory = Console.ReadLine();

            if (string.IsNullOrEmpty(userNameOfAddedCategory))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to add category.");
                Console.ForegroundColor = ConsoleColor.White;
                AddCategory();
            }
            else
            {
                category.CategoryName = userNameOfAddedCategory;
                categoryId = _categoriesService.AddItem(category);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Category {category.CategoryName} with ID={categoryId} was added successfully. ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
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
