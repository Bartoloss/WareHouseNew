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

        public string AddNewItemView(MenuActionService actionService) //do metody "AddNewItem" przekazuje "actionService" ponieważ w tej metodzie chcę, aby było menu
        {
            Console.WriteLine("\nPlease select the category of added item");
            var addNewItemMenu = actionService.GetMenuActionsByMenuName("AddNewItemMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < addNewItemMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{addNewItemMenu[i].Id}. {addNewItemMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string addOperation = Console.ReadLine();
            return addOperation;
        }

        public int AddNewItem(string itemType) //metoda odpowiedzialna za dodanie nowego produktu
        {
            int categoryId;
            Int32.TryParse(itemType, out categoryId);
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
            string userId = Console.ReadLine();
            int id;
            Int32.TryParse(userId, out id);
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

        public int ListOfProductsView(MenuActionService actionService)
        {
            Console.WriteLine("\nPlease select category of products you want to see:");
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

        public void ListOfProducts(int categoryId)
        {
            foreach (var item in Items)
            {
                if (item.CategoryId == categoryId)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}");
                }
            }
            
        }
    }

    
}
