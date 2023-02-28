namespace WareHouseNew
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to warehouse app!");
            ItemService itemService = new ItemService();
            MenuActionService menuActionService = new MenuActionService();
            ItemManager itemManager = new ItemManager(itemService, menuActionService); //utworzenie nowego obiektu klasy "ItemManager"
            
            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:");
                MenuActionService actionService = new MenuActionService(); //stworzenie nowego obiektu "actionService" serwisu
                actionService = Initialize(actionService); //nadpisanie obiektu "actionService" poprzez przypisanie metody, która jest zdefiniowana niżej
                var mainMenu = actionService.GetMenuActionsByMenuName("MainMenu"); //do zmiennej "mainMenu" przypisuje opcje menu z kategorii "Main"
                for(int i = 0; i < mainMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii main
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
                }

                string operation = Console.ReadLine(); //odczyt zmiennej "operation" podanej przez użytkownika
                switch(operation)
                {
                    case "1":
                        int addKeyInfo = itemManager.AddNewItem(actionService); //odbiór informacji na temat wciśniętego przycisku
                        break;
                    case "2":
                        itemManager.RemoveExistItem();
                        break;
                    case "3":
                        itemManager.ListOfProductsView(actionService);
                        //itemService.ListOfProducts(ListKeyInfo);
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Option you have entered does not exist");
                        break;


                }
            }

        }

        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Add product", "MainMenu");
            actionService.AddNewAction(2, "Remove product", "MainMenu");
            actionService.AddNewAction(3, "List of products", "MainMenu");
            actionService.AddNewAction(4, "Show details of product", "MainMenu");

            actionService.AddNewAction(1, "Tshirts", "AddNewItemMenu");
            actionService.AddNewAction(2, "Hoddies", "AddNewItemMenu");
            actionService.AddNewAction(3, "Gadgets", "AddNewItemMenu");
            actionService.AddNewAction(4, "Trousers", "AddNewItemMenu");

            actionService.AddNewAction(1, "List of Tshirts products", "ListOfProductsMenu");
            actionService.AddNewAction(2, "List of Hoddies products", "ListOfProductsMenu");
            actionService.AddNewAction(3, "List of Gadgets products", "ListOfProductsMenu");
            actionService.AddNewAction(4, "List of Trousers products", "ListOfProductsMenu");
            actionService.AddNewAction(5, "List of all products", "ListOfProductsMenu");

            return actionService;  
        }
    }
}
