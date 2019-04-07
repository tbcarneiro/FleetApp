using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces;
using Fleet.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Infrastructure.Data.Repository
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(SqliteContext context)
        {
            this.context = context;
        }

        public Vehicle SelectByChassis(uint chassisNumber)
        {
            return context.Set<Vehicle>().FirstOrDefault(x => x.Chassis.Number == chassisNumber);
        }
    }
}
