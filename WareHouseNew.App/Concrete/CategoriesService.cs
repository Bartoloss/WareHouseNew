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
            Categories? categoryToShow = ObjList.Where(i => i.Id == userChoiceIdOfCategory).FirstOrDefault();
            return categoryToShow;
        }

        public int GetNumberOfAllCategories ()
        {
            int numberOfAllCategories = ObjList.Count;
            return numberOfAllCategories;
        }
    }
}



 