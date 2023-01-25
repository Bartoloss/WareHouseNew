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
            _menuActionService = menuActionService; //wstrzyknięcie do managera dwóch serwisów: menuActionServis oraz ItemService
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
                else
                {

                    Item productToRemove = new Item(); //zadeklarowanie i stworzenie pustego produktu do usunięcia
                    foreach (Item item in _itemService.Items)
                    {
                        if (item.Id == removeId)
                        {
                            productToRemove = item; //nadpisanie znalezionego produktu do wcześniej zadeklarowanego pustego produktu do usunięcia
                            _itemService.RemoveItem(productToRemove); //wysłanie produktu do metody RemoveItem, gdzie nastąpi usunięcie
                            
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Product of id={removeId} was deleted successfully!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.WriteLine("Product of id you entered does not exist. Please write again:");

                        }
                    }
                    
                }
                
            }
            else
            {
                Console.WriteLine("Please enter a number!"); 
            }
        }

        

        public int ListOfProductsView(MenuActionService actionService)
        {
            Console.WriteLine("Please select category of products you want to see:");
            var listOfProductsMenu = actionService.GetMenuActionsByMenuName("ListOfProductsMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < listOfProductsMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{listOfProductsMenu[i].Id}. {listOfProductsMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string listOperation = Console.ReadLine();
            int listOperationInt;
            Int32.TryParse(listOperation, out listOperationInt);
            return listOperationInt;
        }

        
    }

    
}
