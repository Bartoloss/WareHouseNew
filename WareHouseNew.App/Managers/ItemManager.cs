using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Concrete;
using WareHouseNew.App.Helpers;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Managers
{
    public class ItemManager
    {
        public ItemService _itemService;
        public MenuActionService _menuActionService;
        public CategoriesService _categoriesService;
        public CategoriesManager _categoriesManager;

        public ItemManager(ItemService itemService, MenuActionService menuActionService, CategoriesService categoriesService, CategoriesManager categoriesManager)
        {
            _itemService = itemService;
            _menuActionService = menuActionService; //wstrzyknięcie do managera dwóch serwisów: menuActionService oraz ItemService
            _categoriesService = categoriesService;
            _categoriesManager = categoriesManager;
        }

        public void AddNewItem()
        {
            var AddNewItemMenu = _menuActionService.GetMenuActionsByMenuName("AddItemMenu");
            for (int i  = 0; i < AddNewItemMenu.Count; i++)
            {
                Console.WriteLine($"{AddNewItemMenu[i].Name}");
            }
            _categoriesManager.GetAllCategories();
            string userAddChoice = Console.ReadLine();
            int AddChoice;
            {
                if (Int32.TryParse(userAddChoice, out AddChoice))
                {
                    switch (AddChoice)
                    {
                        case > 1:
                        case < 4:
                            Item product = new Item();
                            product.CategoryId = AddChoice;
                            Console.WriteLine("Please enter name for new product:");
                            string userName = Console.ReadLine();
                            product.Name = userName;
                            Console.WriteLine("Please enter amount of new product:");
                            string userAmount = Console.ReadLine();
                            int amount;
                            Int32.TryParse(userAmount, out amount);
                            product.Amount = amount;
                            product.CreatedDate = DateTime.Now;
                            _itemService.AddItem(product);
                            int newId = product.Id;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Product {product.Name} was added successfully with number of id={newId}.");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Error! Please enter the number, not letter or word.");
                }
            }
        }

        public void RemoveExistItem()
        {
            Console.WriteLine("Please enter id of product you want to delete:");
            List<Item> allItems = _itemService.GetAllItems();
            foreach (Item item in allItems)
            {
                Console.WriteLine($"{item.Id}. {item.Name}.");
            }
            string userId = Console.ReadLine();
            int removeId;
            if (Int32.TryParse(userId, out removeId) == true)
            {
                if (removeId < 0)
                {
                    RemoveExistItem();
                }
                else if (removeId > 0)
                {
                    Item? productToRemove = _itemService.GetItemById(removeId);
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
                        Console.WriteLine($"Product of id={removeId} was deleted successfully!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a number!");
            }
        }

        public void ListOfProductsView()
        {
            var ListOfProductsViewMenu = _menuActionService.GetMenuActionsByMenuName("ListOfProductsViewMenu");
            for (int i = 0; i < ListOfProductsViewMenu.Count; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"{ListOfProductsViewMenu[i].Name}");
                }
                else
                {
                    Console.WriteLine($"{ListOfProductsViewMenu[i].Id}.{ListOfProductsViewMenu[i].Name}");
                }
            }

            string userChoiceString = Console.ReadLine();
            int userChoice;
            Int32.TryParse(userChoiceString, out userChoice);
           
            switch(userChoice)
            {
                case 1:
                    List<Item> AllProducts = _itemService.GetAllItems();
                    if(AllProducts.Any())
                    foreach (Item item in AllProducts)
                    {
                        Console.WriteLine($"{item.Id}.{item.Name}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("There are no products with low stack.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                        break;
                case 2:
                    List<Item>? ProductsWithLowStack = _itemService.GetItemsWithLowStack();
                    if (ProductsWithLowStack.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("List of products with low stack:");
                        foreach (Item item in ProductsWithLowStack)
                        {
                            Console.WriteLine($"{item.Id}.{item.Name}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("There are no products with low stack.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;
                case 3:
                    _categoriesManager.GetListOfProducts();
                    string userChoiceCategoryString = Console.ReadLine();
                    int userChoiceCategory;
                    Int32.TryParse(userChoiceCategoryString, out userChoiceCategory);
                    List<Item> returnedProducts = _itemService.GetItemsByCategory(userChoiceCategory);

                    if (returnedProducts.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There are no products in category you selected.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        foreach (Item item in returnedProducts)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"{item.Id}. {item.Name}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    break;
            }
            
        }

        public void ShowDetails()
        {
            List<Item> allItems = _itemService.GetAllItems();
            Console.WriteLine("Please select ID of the product whose details you want to see: ");
            foreach (Item item in allItems)
            {
                Console.WriteLine($"{item.Id}. {item.Name}.");
            }
            string userChoiceString = Console.ReadLine();
            int userChoice;
            Int32.TryParse(userChoiceString, out userChoice);
            Item productToDisplay = _itemService.GetItemById(userChoice);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Product details with ID={productToDisplay.Id}:");
            Console.WriteLine($"Name: {productToDisplay.Name}");
            Console.WriteLine($"Category: {_categoriesService.GetCategoryById(productToDisplay.CategoryId)}");
            Console.WriteLine($"Amount: {productToDisplay.Amount} pcs");
            Console.WriteLine($"Large stock: {productToDisplay.ChangeAmount(productToDisplay.Amount)}");
            Console.WriteLine($"Created Date: {productToDisplay.CreatedDate}");
            Console.WriteLine($"Created by user ID: {User.ID}");
            Console.ForegroundColor = ConsoleColor.White;

        }

    }

    
}
