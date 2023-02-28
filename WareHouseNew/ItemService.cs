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

        public Item? GetItemById(int id) //pytajnik daje się, żeby można było zwrócić nulla, tam gdzie zadeklarowano że będzie zwrócony obiekt
        {
            foreach (Item Item in Items)
            {
                if (Item.Id == id)
                {
                    return Item;
                }
            }
            return null;
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

        
        public List<Item>? GetItemsByCategory(int operation)
        {
            List<Item> productsToShow = new List<Item>();
            if (operation == 5)
            {
                return Items;
            }
            else
            {
                foreach (Item Item in Items)
                {
                    if (Item.CategoryId == operation)
                    {
                        productsToShow.Add(Item);
                    }
                }
                return productsToShow; 
            }
        }

       
        

        }
    }

    

