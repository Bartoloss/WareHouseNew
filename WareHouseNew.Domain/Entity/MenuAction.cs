using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.Domain.Common;

namespace WareHouseNew.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }

        public MenuAction(int id, string name, string menuName)
        {
            Id = id;
            Name = name;
            MenuName = menuName;
        }
    }
    
}
