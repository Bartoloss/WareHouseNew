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
        public Item? GetItemById(int userChoiceIdOfProduct) //pytajnik daje się, żeby można było zwrócić nulla, tam gdzie zadeklarowano że będzie zwrócony obiekt
        {
            foreach (Item product in ObjList)
            {
                if (product.Id == userChoiceIdOfProduct)
                {
                    return product;
                }
            }
            return null;
        }

        public List<Item>? GetItemsByCategory(int userChoiceCategoryOfProducts)
        {
            List<Item> productsToShow = new List<Item>();
            foreach (Item product in ObjList)
            {
                if (product.CategoryId == userChoiceCategoryOfProducts)
                {
                    productsToShow.Add(product);
                }
            }
            return productsToShow;
            

        }

        public List<Item>? GetItemsWithLowStack()
        {
            List<Item> productsWithLowStack = new List<Item>();
            foreach (Item product in ObjList)
            {
                if(product.ChangeAmount(product.Amount) == false)
                {
                    productsWithLowStack.Add(product);
                }
            }
            return productsWithLowStack;
        }
          
    }
}


    

