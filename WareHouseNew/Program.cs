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
            CategoriesService categoriesService = new CategoriesService();
            CategoriesManager categoriesManager = new CategoriesManager(categoriesService);
            ItemManager itemManager = new ItemManager(itemService, menuActionService, categoriesService, categoriesManager); //utworzenie nowego obiektu klasy "ItemManager"

            string userChoiceIdOfUser;
            int idOfUser;
            do
            {
                Console.WriteLine("Please enter your ID user:");
                userChoiceIdOfUser = Console.ReadLine();
            }
            while (Int32.TryParse(userChoiceIdOfUser, out idOfUser) == false);
            Console.WriteLine("Thank you! Hello user of ID=" + idOfUser);
            User.ID = idOfUser;

            string userChoiceAmountOfCategories;
            int amountOfCategories;
            do
            {
                Console.WriteLine("How many categories do you want to enter?:");
                
                userChoiceAmountOfCategories = Console.ReadLine();
            }
            while ((Int32.TryParse(userChoiceAmountOfCategories, out amountOfCategories)) == false);

            for (int i = 1; i <= amountOfCategories; i++)
            {
                categoriesManager.AddCategory();
            }
            
            
            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:");
                var mainMenu = menuActionService.GetMenuActionsByMenuName("MainMenu"); //do zmiennej "mainMenu" przypisuje opcje menu z kategorii "Main"
                for(int i = 0; i < mainMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii main
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
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
                        string userChoiceAmountOfCategoriesToAdd;
                        int amountOfCategoriesToAdd;
                        do
                        {
                            Console.WriteLine("How many categories do you want to enter?:");
                            userChoiceAmountOfCategoriesToAdd = Console.ReadLine();
                        }
                        while ((Int32.TryParse(userChoiceAmountOfCategories, out amountOfCategoriesToAdd)) == false);
                        {
                            for (int i = 1; i <= amountOfCategoriesToAdd; i++)
                            {
                                categoriesManager.AddCategory();
                            }
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
