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
        public Categories? GetCategoryById(int categoryId) 
        {
            Categories? category = ObjList.Where(i => i.Id == categoryId).FirstOrDefault();
            return category;
        }

        public int GetNumberOfAllCategories ()
        {
            int number = ObjList.Count;
            return number;
        }
    }
}



 