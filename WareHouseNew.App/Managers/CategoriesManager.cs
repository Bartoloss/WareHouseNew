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
        public Dictionary<string, string> _pathOfObjects;

        public CategoriesManager(CategoriesService categoriesService, Dictionary<string, string> pathOfObjects)
        {
            _categoriesService = categoriesService;
            _pathOfObjects = pathOfObjects;
        }

        public void LoadProgressOfCategory()
        {
            string loadPath = _pathOfObjects["pathOfCategories"];
            if (File.Exists(loadPath))
            {
                string loadedCategoriesString = File.ReadAllText(loadPath);
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
            string savePath = _pathOfObjects["pathOfCategories"];
            List<Categories> categoriesToSave = _categoriesService.GetAllItems();
            if (categoriesToSave.Count > 0)
            {
                if (File.Exists(savePath))
                {

                }
                else
                {
                    File.Create(savePath).Close();
                }
                string output = JsonConvert.SerializeObject(categoriesToSave); //zapisanie utworzonych produktów przez program do typu string
                using StreamWriter sw2 = new StreamWriter(savePath);
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
            string name;
            
            Console.WriteLine($"Please enter name of {categoryId} category:");
            name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to add category.");
                Console.ForegroundColor = ConsoleColor.White;
                AddCategory();
            }
            else
            {
                category.Name = name;
                category.CreatedById = User.ID;
                categoryId = _categoriesService.AddItem(category);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Category {category.Name} with ID={categoryId} was added successfully. ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }

        public void ViewAllCategories()
        {
            List<Categories> allCategories = _categoriesService.GetAllItems();
            if (allCategories.Any())
            {
                DisplayIdAndNameOfObjects(allCategories);
            }
        }

        public void ViewListOfCategories()
        {
            List<Categories> ListofAllCategories = _categoriesService.GetAllItems();
            if (ListofAllCategories.Any())
            {
                Console.WriteLine("Please select category of products to display:");
                DisplayIdAndNameOfObjects(ListofAllCategories);
            }
        }
    }
}
