using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Domain.Entities
{
    public class VehicleType : BaseEntity
    {
        public string Name { get; set; }
        public byte NumPassengers { get; set; }
    }
}
