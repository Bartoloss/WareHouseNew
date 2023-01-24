using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseNew
{
    public class ItemService
    {
        public List<Item> Items { get; set; }
        public ItemService()
        {
            Items = new List<Item>();
        }
        public void AddNewItem(int categoryId) //metoda odpowiedzialna za dodanie nowego produktu
        {
            Item item = new Item();
            item.CategoryId = categoryId;
            int lastId = GetLastId();
            Console.WriteLine($"The id for new product is {lastId + 1}.");
            item.Id = lastId + 1;
            Console.WriteLine("Please enter name for new product:");
            string userName = Console.ReadLine();
            item.Name = userName;
            Items.Add(item);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Product {userName} was added successfully!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RemoveItem(int removeId) //metoda odpowiedzialna za usunięcie produktu
        {
            if (removeId < 0)
            {
                ItemManager itemManager = new ItemManager(); //utworzenie nowego obiektu klasy "ItemManager"
                itemManager.RemoveItemView();
            }
            else
            {
                Item productToRemove = new Item(); //zadeklarowanie i stworzenie pustego produktu do usunięcia
                foreach (Item product in Items)
                {
                    if (product.Id == removeId)
                    {
                        productToRemove = product; //nadpisanie znalezionego produktu do wcześniej zadeklarowanego pustego produktu do usunięcia
                        break;
                    }
                }
                Items.Remove(productToRemove); //usunięcie produktu
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Product of id={removeId} was deleted successfully!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void ListOfProducts(int categoryId)
        {
            //UPDATE-(5 tydzień - LINQ) w przypadku braku produktów z danej kategorii wyświetlić komunikat o braku produktów

            List<Item> productsToShow = new List<Item>();
            foreach (Item product in Items)
            {
                if (product.CategoryId == categoryId)
                {
                    productsToShow.Add(product);
                }
                else if (categoryId == 5)
                {
                    productsToShow.Add(product);
                }
            }
            if (productsToShow.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (Item product in productsToShow)
                {
                    Console.WriteLine($"{product.Id}. {product.Name}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sory, there are no products in this category.");
                Console.ForegroundColor = ConsoleColor.White;
            }


        }

        public int GetLastId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(p => p.Id).LastOrDefault().Id;
            }
            else
            {
                lastId = 0;
            }
            return lastId;
        }

    }

    
}
