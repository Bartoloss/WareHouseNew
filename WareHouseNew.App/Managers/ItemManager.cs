using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WareHouseNew.App.Abstract;
using WareHouseNew.App.Common;
using WareHouseNew.App.Concrete;
using WareHouseNew.App.Helpers;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Managers
{
    public class ItemManager : BaseManager<Item>
    {
        public ItemService _itemService;
        public MenuActionService _menuActionService;
        public CategoriesService _categoriesService;
        public CategoriesManager _categoriesManager;
        public Dictionary<string, string> _pathOfObjects;

        public ItemManager(ItemService itemService, MenuActionService menuActionService, CategoriesService categoriesService, CategoriesManager categoriesManager, Dictionary<string, string> pathOfObjects)
        {
            _itemService = itemService;
            _menuActionService = menuActionService;
            _categoriesService = categoriesService;
            _categoriesManager = categoriesManager;
            _pathOfObjects = pathOfObjects;
        }

        public void LoadProgressOfItem()
        {
            string loadPath = _pathOfObjects["pathOfProducts"];
            if (File.Exists(loadPath))
            {
                string loadedProductsString = File.ReadAllText(loadPath);
                List<Item>? loadedProducts = JsonConvert.DeserializeObject<List<Item>>(loadedProductsString);
                if (loadedProducts != null)
                {
                    foreach (Item product in loadedProducts)
                    {
                        _itemService.AddItem(product);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Products from 'products.txt' file was added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public bool SaveProgressOfItem()
        {
            string savePath = _pathOfObjects["pathOfProducts"];
            List<Item> productsToSave = _itemService.GetAllItems();
            if (productsToSave.Count > 0)
            {
                if (File.Exists(savePath))
                {

                }
                else
                {
                    File.Create(savePath).Close();
                }
                string output = JsonConvert.SerializeObject(productsToSave); //zapisanie utworzonych produktów przez program do typu string
                using StreamWriter sw1 = new StreamWriter(savePath); 
                using JsonWriter writer1 = new JsonTextWriter(sw1); //potok do zapisywania Jsona

                JsonSerializer serializer = new JsonSerializer(); //utworzenie oddzielnego obiektu serializera
                serializer.Serialize(writer1, productsToSave); //przekazanie potoku do zapisywania Jsona oraz listy elementów
                return true;
            }
            return false;
        }


        public void AddNewItem()
        {
            List<Categories> allCategories = _categoriesService.GetAllItems();
            if (allCategories.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("First you need to add a category.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Please select the category of added item:");
                _categoriesManager.ViewAllCategories();
                string categoryString = Console.ReadLine();
                int category;
                {
                    if (Int32.TryParse(categoryString, out category))
                    {
                        int numberOfCategories = _categoriesService.GetNumberOfAllCategories();
                        if (category <= numberOfCategories)
                        {
                            Item product = new Item();
                            product.CategoryId = category;
                            Console.WriteLine("Please enter name for new product:");
                            string name = Console.ReadLine();
                            if (string.IsNullOrEmpty(name))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Failed to add product.");
                                Console.ForegroundColor = ConsoleColor.White;
                                AddNewItem();
                            }
                            else
                            {
                                product.Name = name;
                                Console.WriteLine("Please enter amount of new product:");
                                string amountString = Console.ReadLine();
                                Int32.TryParse(amountString, out int amount);
                                product.Amount = amount;
                                product.CreatedDate = DateTime.Now;
                                product.CreatedById = User.ID;
                                int id = _itemService.AddItem(product);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Product {product.Name} was added successfully with number of id={id}.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Number of category you entered does not exist.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error! Please enter the number, not letter or word.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        public void RemoveExistItem()
        {
            List<Item> areProducts = _itemService.GetAllItems();
            if (areProducts.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("First you need to add a product.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Please enter id of product you want to delete:");
                List<Item> allProducts = _itemService.GetAllItems();
                DisplayIdAndNameOfObjects(allProducts);
                string idProductToRemoveString = Console.ReadLine();
                if (Int32.TryParse(idProductToRemoveString, out int idProductToRemove))
                {
                    if (idProductToRemove <= 0)
                    {
                        RemoveExistItem();
                    }
                    else if (idProductToRemove > 0)
                    {
                        Item? productToRemove = _itemService.GetItemById(idProductToRemove);
                        if (productToRemove == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Product of id you entered does not exist.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            _itemService.RemoveItem(productToRemove);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Product {productToRemove.Name} of id={productToRemove.Id} was deleted successfully!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error! Please enter the number, not letter or word.");
                }
            }
        }

        public void ListOfProductsView()
        {
            List<Item> allProducts = _itemService.GetAllItems();
            if (allProducts.Count != 0)
            {
                Console.WriteLine("Please select an option for products to display:");
                List<MenuAction> ListOfProductsViewMenu = _menuActionService.GetMenuActionsByMenuName("ListOfProductsViewMenu");
                for (int i = 0; i < ListOfProductsViewMenu.Count; i++)
                {
                    Console.WriteLine($"{ListOfProductsViewMenu[i].Id}.{ListOfProductsViewMenu[i].Name}");
                }
                string menuNumberString = Console.ReadLine();
                if (Int32.TryParse(menuNumberString, out int menuNumber))
                {
                    switch (menuNumber)
                    {
                        case 1:
                            List<Item> allAddedProducts = _itemService.GetAllItems();
                            if (allAddedProducts.Any())
                            {
                                DisplayIdAndNameOfObjects(allAddedProducts);
                            }
                            break;
                        case 2:
                            List<Item>? productsWithLowStack = _itemService.GetItemsWithLowStack();
                            if (productsWithLowStack != null)
                            {
                                Console.WriteLine("List of products with low stack:");
                                DisplayIdAndNameOfObjects(productsWithLowStack);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("There are no products with low stack.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;
                        case 3:
                            _categoriesManager.ViewListOfCategories();
                            string productsCategoryString = Console.ReadLine();
                            Int32.TryParse(productsCategoryString, out int productsCategory);
                            List<Item>? returnedProducts = _itemService.GetItemsByCategory(productsCategory);
                            if (returnedProducts == null) 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("There are no products in category you selected.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                DisplayIdAndNameOfObjects(returnedProducts);
                            }
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Option you entered does not exist.");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Please enter the number, not letter or word.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no added products. Add product first.");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        public void ShowDetails()
        {
            List <Item> allProducts = _itemService.GetAllItems();
            if (allProducts.Count != 0)
            {
            Console.WriteLine("Please select the category to which the product belongs:");
            _categoriesManager.ViewAllCategories();
            string categoryString = Console.ReadLine();

                if (Int32.TryParse(categoryString, out int category))
                {
                    List<Item>? productsByCategory = _itemService.GetItemsByCategory(category);
                    if (productsByCategory == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There are no products in category you chosen.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("Please select ID of the product whose details you want to see: ");
                        DisplayIdAndNameOfObjects(productsByCategory);
                        string idProductString = Console.ReadLine();
                        if (Int32.TryParse(idProductString, out int idProduct))
                        {
                            Item? productToDisplay = _itemService.GetItemById(idProduct);
                            if (productToDisplay != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($"Product details with ID={productToDisplay.Id}:");
                                Console.WriteLine($"Name: {productToDisplay.Name}");
                                Categories? returnCategory = _categoriesService.GetCategoryById(productToDisplay.CategoryId);
                                Console.WriteLine($"Category: {returnCategory?.Name}");
                                Console.WriteLine($"Amount: {productToDisplay.Amount} pcs");
                                Console.WriteLine($"Large stock: {productToDisplay.ChangeAmount(productToDisplay.Amount)}");
                                Console.WriteLine($"Created Date: {productToDisplay.CreatedDate}");
                                Console.WriteLine($"Created by user ID: {User.ID}");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Product of id you entered does not exist.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error! Please enter the number, not letter or word.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Please enter the number, not letter or word.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no added products. Add product first.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }

    }

    
}
