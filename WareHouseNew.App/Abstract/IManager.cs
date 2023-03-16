using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseNew.App.Abstract
{
    public interface IManager<T>
    {
        void DisplayIdAndNameOfObjects(List<T> listOfObjects);
    }
}
