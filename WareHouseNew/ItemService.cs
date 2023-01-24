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

        private int GetLastId()
        {
            return (Items.Any()) ? Items.OrderBy(p => p.Id).LastOrDefault().Id : 0;
        }

        public int AddItem(Item item) //metoda odpowiedzialna za dodanie nowego produktu
        {
            item.Id = GetLastId() + 1;
            Items.Add(item);
            return item.Id;
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
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

       

    }

    
}
