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

        public ItemManager(ItemService itemService, MenuActionService menuActionService, CategoriesService categoriesService)
        {
            _itemService = itemService;
            _menuActionService = menuActionService; //wstrzyknięcie do managera dwóch serwisów: menuActionService oraz ItemService
            _categoriesService = categoriesService;
        }

        //METODY ZWIĄZANE Z PRODUKTAMI
        public void AddNewItem()
        {
            Console.WriteLine("Please select the category of added item");
            var addNewItemMenu = _categoriesService.GetAllItems(); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < addNewItemMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{addNewItemMenu[i].Id}. {addNewItemMenu[i].CategoryName}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string addOperation = Console.ReadLine();
            int addOperationInt;
            {
                if (Int32.TryParse(addOperation, out addOperationInt))
                {
                    switch (addOperationInt)
                    {
                        case >1:
                        case <4:
                            Item product = new Item();
                            product.CategoryId = addOperationInt;
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
            Console.WriteLine("Please select category of products you want to see:");
            var listOfProductsMenu = _menuActionService.GetMenuActionsByMenuName("ListOfProductsMenu");
            for (int i = 0; i < listOfProductsMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{listOfProductsMenu[i].Id}. {listOfProductsMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string userChoiceString = Console.ReadLine();
            int userChoice;
            Int32.TryParse(userChoiceString, out userChoice);
            
            List<Item> returnedProducts = _itemService.GetItemsByCategory(userChoice);

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
            Console.WriteLine($"Amount: {productToDisplay.Amount} pcs");
            Console.WriteLine($"Large stock: {productToDisplay.ChangeAmount(productToDisplay.Amount)}");
            Console.WriteLine($"Created Date: {productToDisplay.CreatedDate}");
            Console.WriteLine($"Created by user ID: {User.ID}");
            Console.ForegroundColor = ConsoleColor.White;

        }


        //METODY ZWIĄZANE Z KATEGORIAMI

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

        public void ViewListOfCategories()
        {
            List<Categories> allCategories = _categoriesService.GetAllItems();
            foreach (Categories category in allCategories)
            {
                Console.WriteLine($"List of {category.CategoryName} products.");
            }
            
        }
    }

    
}
