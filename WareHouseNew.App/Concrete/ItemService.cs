using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Common;
using WareHouseNew.App.Helpers;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Concrete
{
    public class ItemService : BaseService<Item>
    {

        public Item? GetItemById(int userId) //pytajnik daje się, żeby można było zwrócić nulla, tam gdzie zadeklarowano że będzie zwrócony obiekt
        {
            foreach (Item Item in ObjList)
            {
                if (Item.Id == userId)
                {
                    return Item;
                }
            }
            return null;
        }

        public List<Item>? GetItemsByCategory(int userCategory)
        {
            List<Item> productsToShow = new List<Item>();
            if (userCategory == 0)
            {
                return ObjList;
            }
            else
            {
                foreach (Item Item in ObjList)
                {
                    if (Item.CategoryId == userCategory)
                    {
                        productsToShow.Add(Item);
                    }
                }
                return productsToShow;
            }

        }

        public List<Item>? GetItemsWithLowStack()
        {
            List<Item> productsToShow = new List<Item>();
            foreach (Item Item in ObjList)
            {
                if(Item.ChangeAmount(Item.Amount) == false)
                {
                    productsToShow.Add(Item);
                }
            }
            return productsToShow;
        }
          
    }
}


    

