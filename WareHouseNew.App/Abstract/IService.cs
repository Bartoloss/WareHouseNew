using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.Domain.Entity;

namespace WareHouseNew.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }
        int GetLastId();
        int AddItem(T item);
        void RemoveItem(T item);
        List<T> GetAllItems(); 
    }
}
