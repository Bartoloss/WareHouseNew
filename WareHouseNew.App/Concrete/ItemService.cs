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
        public Item? GetItemById(int userChoiceIdOfProduct)
        {
            Item? productToShow = ObjList.Where(i => i.Id == userChoiceIdOfProduct).FirstOrDefault();
            return productToShow;
        } 

        public List<Item>? GetItemsByCategory(int userChoiceCategoryOfProducts)
        {
            List<Item> productsToShow = ObjList.Where(i => i.CategoryId == userChoiceCategoryOfProducts).ToList();
            return productsToShow;
        }

        public List<Item>? GetItemsWithLowStack()
        {
            List<Item> productsWithLowStack = (List<Item>)ObjList.Where(i => i.ChangeAmount(i.Amount) == false).ToList();
            return productsWithLowStack;
        }
    }
}


    

