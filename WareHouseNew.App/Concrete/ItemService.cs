using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Common;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Concrete
{
    public class ItemService : BaseService<Item>
    {

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


    

