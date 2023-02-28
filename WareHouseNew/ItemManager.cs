using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseNew
{
    public class ItemManager
    {
        public ItemService _itemService;
        public MenuActionService _menuActionService;

        public ItemManager(ItemService itemService, MenuActionService menuActionService)
        {
            _itemService = itemService;
            _menuActionService = menuActionService; //wstrzyknięcie do managera dwóch serwisów: menuActionService oraz ItemService
        }

        public int AddNewItem(MenuActionService actionService) //do metody "AddNewItem" przekazuje "actionService" ponieważ w tej metodzie chcę, aby było menu
        {
            Console.WriteLine("Please select the category of added item");
            var addNewItemMenu = actionService.GetMenuActionsByMenuName("AddNewItemMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < addNewItemMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{addNewItemMenu[i].Id}. {addNewItemMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string addOperation = Console.ReadLine();
            int addOperationInt;
            {
                if (Int32.TryParse(addOperation, out addOperationInt) == true)
                {
                    if (addOperationInt == 1 || addOperationInt == 2 || addOperationInt == 3 || addOperationInt == 4)
                    {
                        Item item = new Item();
                        item.CategoryId = addOperationInt;
                        Console.WriteLine("Please enter name for new product:");
                        string userName = Console.ReadLine();
                        item.Name = userName;
                        _itemService.AddItem(item);
                        int newId = item.Id;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Product {item.Name} was added successfully with number of id={newId}.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Error! Please enter the correct number");
                        return -1;
                    }
                }
                else
                {
                    Console.WriteLine("Error! Please enter the number, not letter or word.");
                    return -1;
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
                        Console.ForegroundColor = ConsoleColor.Red;
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


        public void ListOfProductsView(MenuActionService actionService)
        {
            Console.WriteLine("Please select category of products you want to see:");
            var listOfProductsMenu = actionService.GetMenuActionsByMenuName("ListOfProductsMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < listOfProductsMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{listOfProductsMenu[i].Id}. {listOfProductsMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string operationString = Console.ReadLine();
            int operation;
            Int32.TryParse(operationString, out operation);
            
            List<Item> returnedProducts = new List<Item>();
            returnedProducts = _itemService.GetItemsByCategory(operation);

            if (returnedProducts == null)
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

        
        

        
    }

    
}
