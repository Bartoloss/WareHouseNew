using WareHouseNew.App.Abstract;
using WareHouseNew.App.Concrete;
using WareHouseNew.App.Helpers;
using WareHouseNew.Domain.Entity;
using WareHouseNew.App.Managers;

namespace WareHouseNew
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to warehouse app!");
            MenuActionService menuActionService = new MenuActionService(); //stworzenie nowego obiektu "menuActionService" serwisu
            ItemService itemService = new ItemService();    
            ItemManager itemManager = new ItemManager(itemService, menuActionService); //utworzenie nowego obiektu klasy "ItemManager"
            Console.WriteLine("Please enter your ID user:");
            string userName = Console.ReadLine();
            Int32.TryParse(userName, out int userNameInt);
            Console.WriteLine("Thank you! Hello user of ID="+userNameInt);
            User.ID = userNameInt;

            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:");
                var mainMenu = menuActionService.GetMenuActionsByMenuName("MainMenu"); //do zmiennej "mainMenu" przypisuje opcje menu z kategorii "Main"
                for(int i = 0; i < mainMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii main
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
                }

                string operation = Console.ReadLine(); //odczyt zmiennej "operation" podanej przez użytkownika
                switch(operation)
                {
                    case "1":
                        itemManager.AddNewItem(menuActionService);
                        break;
                    case "2":
                        itemManager.RemoveExistItem();
                        break;
                    case "3":
                        itemManager.ListOfProductsView(menuActionService);
                        break;
                    case "4":
                        itemManager.ShowDetails();
                        break;
                    default:
                        Console.WriteLine("Option you have entered does not exist");
                        break;


                }
            }

        }

        
    }
}
