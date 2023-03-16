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
        
        public int Amount { get; set; }

        private bool isLowInWareHouse;
        
        public bool ChangeAmount(int amount)
        {
            Amount = amount;
            if (amount > 10)
            {
                isLowInWareHouse = true;
            }
            return isLowInWareHouse;
        }
    }
}
