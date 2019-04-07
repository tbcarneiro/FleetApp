using Fleet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Domain.Interfaces
{
    public interface IVehicleService : IService<Vehicle>
    {
        Vehicle GetByChassis(uint chassisNumber);
    }
}
