using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.Domain.Common;

namespace WareHouseNew.Domain.Entity
{
    public class Item : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        protected bool isLowInWareHouse;

    }
}
