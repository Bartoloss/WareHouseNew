namespace WareHouseNew
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to warehouse app!");
            while(true)
            {
                Console.WriteLine("Please let me know what you want to do:");
                MenuActionService actionService = new MenuActionService(); //stworzenie nowego obiektu "actionService" serwisu
                actionService = Initialize(actionService); //nadpisanie obiektu "actionService" poprzez przypisanie metody, która jest zdefiniowana niżej
                var mainMenu = actionService.GetMenuActionsByMenuName("MainMenu"); //do zmiennej "mainMenu" przypisuje opcje menu z kategorii "Main"
                for(int i = 0; i < mainMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii main
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
                }

                var operation = Console.ReadKey(); //odczyt zmiennej "operation" podanej przez użytkownika
                ItemManager itemManager = new ItemManager(); //utworzenie nowego obiektu klasy "ItemManager"
                switch(operation.KeyChar)
                {
                    case '1':
                        var keyInfo = itemManager.AddNewItemView(actionService); //odbiór informacji na temat wciśniętego przycisku
                        var addId = itemManager.AddNewItem(keyInfo.KeyChar); //wywołanie metody "AddNewItem" i przekazanie informacji dostarczonej przez użytkownika
                        break;
                    case '2':
                        var removeId = itemManager.RemoveItemView();
                        itemManager.RemoveItem(removeId);
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    default:
                        Console.WriteLine("Option you have entered does not exist");
                        break;
                }
            }

        }

        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Add item", "MainMenu");
            actionService.AddNewAction(1, "Remove item", "MainMenu");
            actionService.AddNewAction(1, "Show details", "MainMenu");
            actionService.AddNewAction(1, "List of Items", "MainMenu");

            actionService.AddNewAction(1, "Tshirts", "AddNewItemMenu");
            actionService.AddNewAction(1, "Hoddies", "AddNewItemMenu");
            actionService.AddNewAction(1, "Gadgets", "AddNewItemMenu");
            actionService.AddNewAction(1, "Trousers", "AddNewItemMenu");
            return actionService;  
        }
    }
}
