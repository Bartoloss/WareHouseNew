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
        public Dictionary<string, string> _localizationOfObjects;

        public ItemManager(ItemService itemService, MenuActionService menuActionService, CategoriesService categoriesService, CategoriesManager categoriesManager, Dictionary<string, string> localizationOfObjects)
        {
            _itemService = itemService;
            _menuActionService = menuActionService;
            _categoriesService = categoriesService;
            _categoriesManager = categoriesManager;
            _localizationOfObjects = localizationOfObjects;
        }
        
        public void LoadProgressOfItem()
        {
            var path = _localizationOfObjects.Where(i => i.Key == "localizationOfProducts").Select(i => i.Value);
            string pathString = Convert.ToString(path);
            if (File.Exists(pathString))
            {
                string loadedProductsString = File.ReadAllText(pathString);
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
            var path = _localizationOfObjects.Where(i => i.Key == "localizationOfProducts").Select(i => i.Value);
            string pathString = Convert.ToString(path);
            List<Item> productsToSave = _itemService.GetAllItems();
            if (productsToSave.Count > 0)
            {
                if (File.Exists(pathString))
                {

                }
                else
                {
                    File.Create(pathString).Close();
                }
                string output = JsonConvert.SerializeObject(productsToSave); //zapisanie utworzonych produktów przez program do typu string
                using StreamWriter sw1 = new StreamWriter(pathString); 
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
                string userChoiceCategory = Console.ReadLine();
                int choiceCategory;
                {
                    if (Int32.TryParse(userChoiceCategory, out choiceCategory))
                    {
                        int numberOfCategories = _categoriesService.GetNumberOfAllCategories();
                        if (choiceCategory <= numberOfCategories)
                        {
                            Item product = new Item();
                            product.CategoryId = choiceCategory;
                            Console.WriteLine("Please enter name for new product:");
                            string userChoiceNameOfProduct = Console.ReadLine();
                            if (string.IsNullOrEmpty(userChoiceNameOfProduct))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Failed to add product.");
                                Console.ForegroundColor = ConsoleColor.White;
                                AddNewItem();
                            }
                            else
                            {
                                product.Name = userChoiceNameOfProduct;
                                Console.WriteLine("Please enter amount of new product:");
                                string userChoiceAmountOfProduct = Console.ReadLine();
                                int amountOfProduct;
                                Int32.TryParse(userChoiceAmountOfProduct, out amountOfProduct);
                                product.Amount = amountOfProduct;
                                product.CreatedDate = DateTime.Now;
                                product.CreatedById = User.ID;
                                int addedProductId = _itemService.AddItem(product);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Product {product.Name} was added successfully with number of id={addedProductId}.");
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
                string userchoiceIdOfProductToRemove = Console.ReadLine();
                int idOfProductToRemove;
                if (Int32.TryParse(userchoiceIdOfProductToRemove, out idOfProductToRemove) == true)
                {
                    if (idOfProductToRemove <= 0)
                    {
                        RemoveExistItem();
                    }
                    else if (idOfProductToRemove > 0)
                    {
                        Item? productToRemove = _itemService.GetItemById(idOfProductToRemove);
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
                var ListOfProductsViewMenu = _menuActionService.GetMenuActionsByMenuName("ListOfProductsViewMenu");
                for (int i = 0; i < ListOfProductsViewMenu.Count; i++)
                {
                    Console.WriteLine($"{ListOfProductsViewMenu[i].Id}.{ListOfProductsViewMenu[i].Name}");
                }
                string userChoiceNumberOfMenu = Console.ReadLine();
                int numberOfMenu;
                if (Int32.TryParse(userChoiceNumberOfMenu, out numberOfMenu))
                {
                    switch (numberOfMenu)
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
                            string userChoiceCategoryOfProducts = Console.ReadLine();
                            int CategoryOfProducts;
                            Int32.TryParse(userChoiceCategoryOfProducts, out CategoryOfProducts);
                            List<Item>? returnedProducts = _itemService.GetItemsByCategory(CategoryOfProducts);
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
            string userChoiceCategoryOfProductToDisplay = Console.ReadLine();
            int CategoryOfProductToDisplay;

                if (Int32.TryParse(userChoiceCategoryOfProductToDisplay, out CategoryOfProductToDisplay))
                {
                    List<Item>? productsByCategory = _itemService.GetItemsByCategory(CategoryOfProductToDisplay);
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
                        string userChoiceIdOfProductToDisplay = Console.ReadLine();
                        int IdOfProductToDisplay;
                        if (Int32.TryParse(userChoiceIdOfProductToDisplay, out IdOfProductToDisplay))
                        {
                            Item? productToDisplay = _itemService.GetItemById(IdOfProductToDisplay);
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
