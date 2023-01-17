using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseNew
{
    public class ItemManager
    {
        public List<Item> Items { get; set; }
        public ItemManager()
        {
            Items = new List<Item>();
        }

        public ConsoleKeyInfo AddNewItemView(MenuActionService actionService) //do metody "AddNewItem" przekazuje "actionService" ponieważ w tej metodzie chcę, aby było menu
        {
            Console.WriteLine("\nPlease select the category of added item");
            var addNewItemMenu = actionService.GetMenuActionsByMenuName("AddNewItemMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < addNewItemMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{addNewItemMenu[i].Id}. {addNewItemMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            var addOperation = Console.ReadKey();
            return addOperation;
        }

        public int AddNewItem(char itemType) //metoda odpowiedzialna za dodanie nowego produktu
        {
            int categoryId;
            Int32.TryParse(itemType.ToString(), out categoryId);
            Item item = new Item();
            item.CategoryId = categoryId;
            Console.WriteLine("\nPlease enter number of id for new product:");
            var userId = Console.ReadKey();
            int addId;
            Int32.TryParse(userId.ToString(), out addId);
            item.Id = addId;
            Console.WriteLine("\nPlease enter name for new product:");
            var userName = Console.ReadLine();
            item.Name = userName;
            Console.WriteLine($"Product {userName} was added successfully!");

            Items.Add(item);
            return addId; 
        }

        public int RemoveItemView()
        {
            Console.WriteLine("\nPlease enter id of product you want to delete:");
            var userId = Console.ReadKey();
            int id;
            Int32.TryParse(userId.KeyChar.ToString(), out id);
            return id;
        }

        public void RemoveItem(int removeId) //metoda odpowiedzialna za usunięcie produktu
        {
            Item productToRemove = new Item(); //zadeklarowanie i stworzenie pustego produktu do usunięcia
            foreach(var item in Items)
            {
                if (item.Id == removeId)
                {
                    productToRemove = item; //nadpisanie znalezionego produktu do wcześniej zadeklarowanego pustego produktu do usunięcia
                    break;
                }
            }
            Items.Remove(productToRemove); //usunięcie produktu
            Console.WriteLine($"\nProduct of id={removeId} was deleted successfully!");
        }

        public ConsoleKeyInfo ListOfProductsView(MenuActionService actionService)
        {
            Console.WriteLine("\nPlease select category of products you want to see:");
            var listOfProductsMenu = actionService.GetMenuActionsByMenuName("ListOfProductsMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < listOfProductsMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{listOfProductsMenu[i].Id}. {listOfProductsMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            var listOperation = Console.ReadKey();
            return listOperation;
        }

        public void ListOfProducts(char categoryId)
        {
            int newCategoryId;
            Int32.TryParse(categoryId.ToString(), out newCategoryId);
            foreach (var item in Items)
            {
                if (item.CategoryId == newCategoryId)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}");
                }
            }
        }
    }

    
}
