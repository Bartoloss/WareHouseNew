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
        public Categories? GetCategoryById(int userChoiceIdOfCategory) 
        {
            foreach (Categories category in ObjList)
            {
                if (category.Id == userChoiceIdOfCategory)
                {
                    return category;
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



 