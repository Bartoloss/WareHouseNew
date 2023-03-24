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
            Dictionary<string, string> pathOfObjects = new Dictionary<string, string>();
            pathOfObjects.Add("pathOfProducts", @"C:\Temp\products.txt");
            pathOfObjects.Add("pathOfCategories", @"C:\Temp\categories.txt");

            Console.WriteLine("Welcome to warehouse app!");
            MenuActionService menuActionService = new MenuActionService(); //stworzenie nowego obiektu "menuActionService" serwisu
            ItemService itemService = new ItemService();
            CategoriesService categoriesService = new CategoriesService();
            CategoriesManager categoriesManager = new CategoriesManager(categoriesService, pathOfObjects);
            ItemManager itemManager = new ItemManager(itemService, menuActionService, categoriesService, categoriesManager, pathOfObjects); //utworzenie nowego obiektu klasy "ItemManager"

            itemManager.LoadProgressOfItem();
            categoriesManager.LoadProgressOfCategory();

            string userIdString;
            int userId;
            do
            {
                Console.WriteLine("Please enter your ID user:");
                userIdString = Console.ReadLine();
            }
            while (!Int32.TryParse(userIdString, out userId));
            Console.WriteLine("Thank you! Hello user of ID=" + userId);
            User.ID = userId;

            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:");
                var mainMenu = menuActionService.GetMenuActionsByMenuName("MainMenu"); 
                for(int i = 0; i < mainMenu.Count; i++) 
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}"); 
                }

                string operation = Console.ReadLine();
                switch(operation)
                {
                    case "1":
                        itemManager.AddNewItem();
                        break;
                    case "2":
                        itemManager.RemoveExistItem();
                        break;
                    case "3":
                        itemManager.ListOfProductsView();
                        break;
                    case "4":
                        itemManager.ShowDetails();
                        break;
                    case "5":
                        string quantityOfCategoriesString;
                        int quantityOfCategories;
                        do
                        {
                            Console.WriteLine("How many categories do you want to enter?:");
                            quantityOfCategoriesString = Console.ReadLine();
                        }
                        while (!Int32.TryParse(quantityOfCategoriesString, out quantityOfCategories));
                        {
                            for (int i = 1; i <= quantityOfCategories; i++)
                            {
                                categoriesManager.AddCategory();
                            }
                        }
                        break;
                    case "6":
                        bool itemsResult = itemManager.SaveProgressOfItem();
                        bool categoriesResult = categoriesManager.SaveProgressOfCategory();
                        if (itemsResult && categoriesResult == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Progress of items and categories was saved succesfully.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (itemsResult == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Progress of items was saved succesfully.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (categoriesResult == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Progress of categories was saved succesfully.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Sorry. Progress could not be saved. Please add category or product.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    default:
                        Console.WriteLine("Option you have entered does not exist");
                        break;


                }
            }

        }

        
    }
}
