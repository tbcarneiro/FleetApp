using FluentValidation;
using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces;
using Fleet.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Service.Services
{
    public class VehicleService : BaseService<Vehicle>, IVehicleService
    {
        private VehicleRepository vehicleRepository;

        public VehicleService()
        {
            vehicleRepository = new VehicleRepository(repository.context);
        }
        public Vehicle GetByChassis(uint chassisNumber)
        {
            if (chassisNumber == 0)
                throw new ArgumentException("The Chassis Number cannot be zero.");

            return vehicleRepository.SelectByChassis(chassisNumber);
        }
    }
}
