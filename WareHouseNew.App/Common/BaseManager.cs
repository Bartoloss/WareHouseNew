using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.Domain.Entity;
using WareHouseNew.App.Abstract;
using WareHouseNew.Domain.Common;

namespace WareHouseNew.App.Common
{
    public class BaseManager<T> : IManager<T> where T : BaseEntity
    {
        public void DisplayIdAndNameOfObjects(List<T> listOfObjects)
        {
            foreach (T item in listOfObjects)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{item.Id}. {item.Name}.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


    }
}
