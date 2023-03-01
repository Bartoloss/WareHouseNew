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
        public List<T> Items { get ; set ; }

        public BaseService()
        {
            Items = new List<T>();
        }

        public int GetLastId()
        {
            return (Items.Any()) ? Items.OrderBy(p => p.Id).LastOrDefault().Id : 0;
        }

        public int AddItem(T item) 
        {
            item.Id = GetLastId() + 1;
            Items.Add(item);
            return item.Id;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

    }
}
