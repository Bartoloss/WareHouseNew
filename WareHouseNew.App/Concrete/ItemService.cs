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
        public Item? GetItemById(int productId)
        {
            Item? product = ObjList.Where(i => i.Id == productId).FirstOrDefault();
            return product;
        } 

        public List<Item>? GetItemsByCategory(int category)
        {
            List<Item> products = ObjList.Where(i => i.CategoryId == category).ToList();
            return products;
        }

        public List<Item>? GetItemsWithLowStack()
        {
            List<Item> products = (List<Item>)ObjList.Where(i => i.ChangeAmount(i.Amount) == false).ToList();
            return products;
        }
    }
}


    

