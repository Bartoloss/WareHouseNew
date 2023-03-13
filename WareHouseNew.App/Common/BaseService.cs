using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Abstract;
using WareHouseNew.Domain.Common;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> ObjList { get ; set ; }

        public BaseService()
        {
            ObjList = new List<T>();
        }

        public int GetLastId()
        {
            return (ObjList.Any()) ? ObjList.OrderBy(p => p.Id).LastOrDefault().Id : 0;
        }

        public int AddItem(T item) 
        {
            item.Id = GetLastId() + 1;
            ObjList.Add(item);
            return item.Id;
        }

        public void RemoveItem(T item)
        {
            ObjList.Remove(item);
        }

        public List<T> GetAllItems()
        {
            return ObjList;
        }

    }
}
