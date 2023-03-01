using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseNew.Domain.Common
{
    public class AuditableModel
    {
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDataTime { get; set; }
    }
}
