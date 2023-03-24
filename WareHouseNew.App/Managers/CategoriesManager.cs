using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Common;
using WareHouseNew.App.Concrete;
using WareHouseNew.App.Helpers;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Managers
{
    public class CategoriesManager : BaseManager<Categories>
    {
        public CategoriesService _categoriesService;
        public Dictionary<string, string> _localizationOfObjects;

        public CategoriesManager(CategoriesService categoriesService, Dictionary<string, string> localizationOfObjects)
        {
            _categoriesService = categoriesService;
            _localizationOfObjects = localizationOfObjects;
        }

        public void LoadProgressOfCategory()
        {
            var path = _localizationOfObjects.Where(i => i.Key == "localizationOfCategories").Select(i => i.Value);
            string pathString = Convert.ToString(path);
            if (File.Exists(pathString))
            {
                string loadedCategoriesString = File.ReadAllText(pathString);
                List<Categories>? loadedCategories = JsonConvert.DeserializeObject<List<Categories>>(loadedCategoriesString);
                if (loadedCategories != null)
                {
                    foreach (Categories category in loadedCategories)
                    {
                        _categoriesService.AddItem(category);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Categories from 'categories.txt' file was added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public bool SaveProgressOfCategory()
        {
            var path = _localizationOfObjects.Where(i => i.Key == "localizationOfCategories").Select(i => i.Value);
            string pathString = Convert.ToString(path);
            List<Categories> categoriesToSave = _categoriesService.GetAllItems();
            if (categoriesToSave.Count > 0)
            {
                if (File.Exists(pathString))
                {

                }
                else
                {
                    File.Create(pathString).Close();
                }
                string output = JsonConvert.SerializeObject(categoriesToSave); //zapisanie utworzonych produktów przez program do typu string
                using StreamWriter sw2 = new StreamWriter(pathString);
                using JsonWriter writer2 = new JsonTextWriter(sw2); //potok do zapisywania Jsona

                JsonSerializer serializer = new JsonSerializer(); //utworzenie oddzielnego obiektu serializera
                serializer.Serialize(writer2, categoriesToSave); //przekazanie potoku do zapisywania Jsona oraz listy elementów
                return true;
            }
            return false;
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
                category.Name = userNameOfAddedCategory;
                category.CreatedById = User.ID;
                categoryId = _categoriesService.AddItem(category);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Category {category.Name} with ID={categoryId} was added successfully. ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }

        public void ViewAllCategories()
        {
            var allCategories = _categoriesService.GetAllItems();
            if (allCategories.Any())
            {
                DisplayIdAndNameOfObjects(allCategories);
            }
        }

        public void ViewListOfCategories()
        {
            var ListofAllCategories = _categoriesService.GetAllItems();
            if (ListofAllCategories.Any())
            {
                Console.WriteLine("Please select category of products to display:");
                DisplayIdAndNameOfObjects(ListofAllCategories);
            }
        }
    }
}
