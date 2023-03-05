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
            Console.WriteLine("Please select the category of added item:");
            _categoriesManager.ViewAllCategories();
            string userChoiceCategory = Console.ReadLine();
            int choiceCategory;
            {
                if (Int32.TryParse(userChoiceCategory, out choiceCategory))
                {
                    int numberOfCategories = _categoriesService.GetNumberOfCategories();
                    if (choiceCategory <= numberOfCategories)
                    {
                        Item product = new Item();
                        product.CategoryId = choiceCategory;
                        Console.WriteLine("Please enter name for new product:");
                        string userChoiceName = Console.ReadLine();
                        product.Name = userChoiceName;
                        Console.WriteLine("Please enter amount of new product:");
                        string userChoiceAmount = Console.ReadLine();
                        int amount;
                        Int32.TryParse(userChoiceAmount, out amount);
                        product.Amount = amount;
                        product.CreatedDate = DateTime.Now;
                        int addedProductId = _itemService.AddItem(product);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Product {product.Name} was added successfully with number of id={addedProductId}.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("Number of category you entered does not exist.");
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
                Console.WriteLine("Please enter a number");
            }
        }

        public void ListOfProductsView()
        {
            Console.WriteLine("Please select an option for products to display:"); 
            var ListOfProductsViewMenu = _menuActionService.GetMenuActionsByMenuName("ListOfProductsViewMenu");
            for (int i = 0; i < ListOfProductsViewMenu.Count; i++)
            {

                Console.WriteLine($"{ListOfProductsViewMenu[i].Id}.{ListOfProductsViewMenu[i].Name}");

            }

            string userChoiceMenuString = Console.ReadLine();
            int userChoiceMenu;
            Int32.TryParse(userChoiceMenuString, out userChoiceMenu);
           
            switch(userChoiceMenu)
            {
                case 1:
                    List<Item> AllProducts = _itemService.GetAllItems();
                    if (AllProducts.Any())
                    {
                        foreach (Item item in AllProducts)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"{item.Id}.{item.Name}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
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
                    _categoriesManager.ViewListOfCategories();
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
            Console.WriteLine("Please select the category to which the product belongs:");
            _categoriesManager.ViewAllCategories();
            string userChoiceCategoryOfProductToDisplay = Console.ReadLine();
            int ChoiceCategoryOfProductToDisplay;

            if (Int32.TryParse(userChoiceCategoryOfProductToDisplay, out ChoiceCategoryOfProductToDisplay))
            {
                List<Item> itemsByCategory = _itemService.GetItemsByCategory(ChoiceCategoryOfProductToDisplay);
                Console.WriteLine("Please select ID of the product whose details you want to see: ");
                foreach (Item item in itemsByCategory)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}.");
                }
                string userChoiceIdOfProductToDisplay = Console.ReadLine();
                int choiceIdOfProductToDisplay;
                Int32.TryParse(userChoiceIdOfProductToDisplay, out choiceIdOfProductToDisplay);
                Item productToDisplay = _itemService.GetItemById(choiceIdOfProductToDisplay);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Product details with ID={productToDisplay.Id}:");
                Console.WriteLine($"Name: {productToDisplay.Name}");
                Console.WriteLine($"Category: {_categoriesService.GetCategoryByName(productToDisplay.CategoryId)}");
                Console.WriteLine($"Amount: {productToDisplay.Amount} pcs");
                Console.WriteLine($"Large stock: {productToDisplay.ChangeAmount(productToDisplay.Amount)}");
                Console.WriteLine($"Created Date: {productToDisplay.CreatedDate}");
                Console.WriteLine($"Created by user ID: {User.ID}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }

    }

    
}
