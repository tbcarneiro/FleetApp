using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public virtual Chassis Chassis { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public string Color { get; set; }
    }

    public class Chassis
    {
        public int VehicleID { get; set; }
        public string Series { get; set; }
        public uint Number { get; set; }
    }
}
