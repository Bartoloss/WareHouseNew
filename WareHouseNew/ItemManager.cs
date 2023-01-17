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
            Console.WriteLine("Please select the category of added item");
            var addNewItemMenu = actionService.GetMenuActionsByMenuName("AddNewItemMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < addNewItemMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{addNewItemMenu[i].Id}. {addNewItemMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            var operation = Console.ReadKey();
            return operation;
        }

        public int AddNewItem(char itemType) //metoda odpowiedzialna za dodanie nowego produktu
        {
            int categoryId;
            Int32.TryParse(itemType.ToString(), out categoryId);
            Item item = new Item();
            item.CategoryId = categoryId;
            Console.WriteLine("Please enter number of id for new item:");
            var userId = Console.ReadKey();
            int addId;
            Int32.TryParse(userId.ToString(), out addId);
            item.Id = addId;
            Console.WriteLine("Please enter name for new item:");
            var userName = Console.ReadLine();
            item.Name = userName;

            Items.Add(item);
            return addId; 
        }

        public int RemoveItemView()
        {
            Console.WriteLine("Please enter id of product you want to delete:");
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
        }
    }
}
