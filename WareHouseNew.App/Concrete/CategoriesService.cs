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
    public class CategoriesService : BaseService<Categories>
    {
        public string? GetCategoryByName(int id)
        {
            foreach (Categories category in ObjList)
            {
                if (category.Id == id)
                {
                    return category.CategoryName;
                }
            }
            return null;
        }

        public int GetNumberOfAllCategories ()
        {
            int numberOfAllCategories = 0;
            foreach (Categories category in ObjList)
            {
                numberOfAllCategories++;
            }
            return numberOfAllCategories;
        }
    }
}



 