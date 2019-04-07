using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fleet.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleet.Domain.Entities;

namespace Fleet.Tests
{
    [TestClass()]
    public class VehicleTests
    {
        static VehicleService vehicleService = new VehicleService();
        static BaseService<VehicleType> vehicleTypeService = new BaseService<VehicleType>();
        static IList<VehicleType> types;

        public VehicleTests()
        {
            VehicleType car = new VehicleType();
            car.Name = "car";
            car.NumPassengers = 4;

            VehicleType truck = new VehicleType();
            truck.Name = "truck";
            truck.NumPassengers = 1;

            VehicleType bus = new VehicleType();
            bus.Name = "bus";
            bus.NumPassengers = 42;

            vehicleTypeService.Post(car);
            vehicleTypeService.Post(truck);
            vehicleTypeService.Post(bus);

            types = vehicleTypeService.Get();
        }

        [TestMethod()]
        public void AddVehicle()
        {
            Vehicle vehicle1 = new Vehicle();
            vehicle1.Color = "red";
            vehicle1.VehicleType = types.FirstOrDefault(x => x.Name == "car");

            vehicle1.Chassis = new Chassis();
            vehicle1.Chassis.Number = 12345;
            vehicle1.Chassis.Series = "xyz";

            vehicleService.Post(vehicle1);

            Assert.IsTrue(vehicle1.Id > 0);
        }

        [TestMethod()]
        public void FindVehicleValid()
        {
            int id = 1;
            var vehicle = vehicleService.GetByChassis(12345);
            Assert.AreEqual(vehicle.Id, id);
        }

        [TestMethod()]
        public void FindVehicleNotFound()
        {
            int id = 2;
            var vehicle = vehicleService.GetByChassis(12345);
            Assert.AreNotEqual(vehicle.Id, id);
        }

        [TestMethod()]
        public void EditVehicle()
        {
            var vehicle = vehicleService.GetByChassis(12345);
            string newColor = "unitTestColor";
            vehicle.Color = newColor;

            vehicleService.Put(vehicle);

            var vehicleRetrieve = vehicleService.GetByChassis(12345);
            Assert.AreEqual(vehicleRetrieve.Color, newColor);
        }

        [TestMethod()]
        public void DeleteVehicle()
        {
            var vehicle = vehicleService.GetByChassis(12345);
            vehicleService.Delete(vehicle.Id);

            var vehicleRetrieve = vehicleService.GetByChassis(12345);
            Assert.IsNull(vehicleRetrieve);
        }
    }
}