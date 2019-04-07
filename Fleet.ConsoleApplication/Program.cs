
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleet.Domain.Entities;
using Fleet.Service;
using Fleet.Service.Services;
using Fleet.Service.Validators;

namespace Fleet.ConsoleApplication
{
    class Program
    {
        static VehicleService vehicleService = new VehicleService();
        static BaseService<VehicleType> vehicleTypeService = new BaseService<VehicleType>();
        static IList<VehicleType> types;

        static void Main(string[] args)
        {

            Init();
            Console.WriteLine("Welcome to Fleet Application");
            Trace.TraceInformation("User started the application");
            Menu();

        }

        static void Init()
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

            Vehicle vehicle1 = new Vehicle();
            vehicle1.Color = "red";
            vehicle1.VehicleType = types.FirstOrDefault(x => x.Name == "car");

            vehicle1.Chassis = new Chassis();
            vehicle1.Chassis.Number = 12345;
            vehicle1.Chassis.Series = "xyz";

            vehicleService.Post<VehicleValidator>(vehicle1);

            Vehicle vehicle2 = new Vehicle();
            vehicle2.Color = "white";
            vehicle2.VehicleType = types.FirstOrDefault(x => x.Name == "truck");

            vehicle2.Chassis = new Chassis();
            vehicle2.Chassis.Number = 54321;
            vehicle2.Chassis.Series = "qwert";

            vehicleService.Post<VehicleValidator>(vehicle2);

            Vehicle vehicle3 = new Vehicle();
            vehicle3.Color = "black";
            vehicle3.VehicleType = types.FirstOrDefault(x => x.Name == "bus");

            vehicle3.Chassis = new Chassis();
            vehicle3.Chassis.Number = 09876;
            vehicle3.Chassis.Series = "asdf";

            vehicleService.Post<VehicleValidator>(vehicle3);

            Vehicle vehicle4 = new Vehicle();
            vehicle4.Color = "silver";
            vehicle4.VehicleType = types.FirstOrDefault(x => x.Name == "car");

            vehicle4.Chassis = new Chassis();
            vehicle4.Chassis.Number = 56789;
            vehicle4.Chassis.Series = "zxcv";

            vehicleService.Post<VehicleValidator>(vehicle4);

            Vehicle vehicle5 = new Vehicle();
            vehicle5.Color = "yellow";
            vehicle5.VehicleType = types.FirstOrDefault(x => x.Name == "truck");

            vehicle5.Chassis = new Chassis();
            vehicle5.Chassis.Number = 13579;
            vehicle5.Chassis.Series = "qazxw";

            vehicleService.Post<VehicleValidator>(vehicle5);
        }

        static void Menu()
        {
            Console.WriteLine("Please input the option number:");
            Console.WriteLine("1 - Insert a new vehicle");
            Console.WriteLine("2 - Edit an existing vehicle");
            Console.WriteLine("3 - Delete an existing vehicle");
            Console.WriteLine("4 - List all vehicles");
            Console.WriteLine("5 - Find a vehicle by chassis");
            Console.WriteLine("6 - Exit");

            var consoleInput = Console.ReadLine();

            bool valid = false;

            while(!valid)
            {
                switch (consoleInput)
                {
                    case "1":
                        valid = true;
                        Insert();
                        break;
                    case "2":
                        valid = true;
                        Edit();
                        break;
                    case "3":
                        valid = true;
                        Delete();
                        break;
                    case "4":
                        valid = true;
                        List();
                        break;
                    case "5":
                        valid = true;
                        Find();
                        break;
                    case "6":
                        valid = true;
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Please input a valid option:");
                        consoleInput = Console.ReadLine();
                        break;
                }
            }
        }

        static void Insert()
        {
            try
            {
                string consoleInput;
                Console.Clear();

                Vehicle vehicle = new Vehicle();
                vehicle.Chassis = new Chassis();

                Console.WriteLine("ADD A NEW VEHICLE");
                Trace.TraceInformation("User entered the Insert option");

                Console.WriteLine("");
                Console.WriteLine("Please input the Chassis Number:");
                consoleInput = Console.ReadLine();

                while (!uint.TryParse(consoleInput, out uint result))
                {
                    Console.WriteLine("Not a valid number, try again:");

                    consoleInput = Console.ReadLine();
                }
                vehicle.Chassis.Number = Convert.ToUInt32(consoleInput);

                Console.WriteLine("");
                Console.WriteLine("Please input the Chassis Series:");
                consoleInput = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(consoleInput))
                {
                    Console.WriteLine("Please input the Chassis Series:");

                    consoleInput = Console.ReadLine();
                }
                vehicle.Chassis.Series = consoleInput;

                Console.WriteLine("");
                Console.WriteLine("Please input vehicle type (car, truck or bus):");
                consoleInput = Console.ReadLine();
                VehicleType type = new VehicleType();
                bool valid = false;

                while (!valid)
                {
                    type = types.FirstOrDefault(x => x.Name == consoleInput);
                    if (type == null)
                    {
                        Console.WriteLine("Please input vehicle type (car, truck or bus):");
                        consoleInput = Console.ReadLine();
                    }
                    else
                    {
                        valid = true;
                    }
                }
                vehicle.VehicleType = type;

                Console.WriteLine("");
                Console.WriteLine("Please input the Vehicle color:");
                consoleInput = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(consoleInput))
                {
                    Console.WriteLine("Please input the Vehicle color:");

                    consoleInput = Console.ReadLine();
                }
                vehicle.Color = consoleInput;

                Trace.TraceInformation("User will insert the vehiche:");
                Trace.TraceInformation("    Chassis Number: {0}", vehicle.Chassis.Number);
                Trace.TraceInformation("    Chassis Series: {0}", vehicle.Chassis.Series);
                Trace.TraceInformation("    Type: {0}", vehicle.VehicleType.Name);
                Trace.TraceInformation("    Color: {0}", vehicle.Color);
                vehicleService.Post<VehicleValidator>(vehicle);

                Console.WriteLine("Vehicle added successfully! Please hit Enter to continue.");
                Trace.TraceInformation("Vehicle added successfully");
                Console.ReadLine();
                Console.Clear();

                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error while adding vehicle. Please check log files");
                Trace.TraceInformation("error while adding vehicle: {0}", ex.Message);
                Console.ReadLine();
                Console.Clear();

                Menu();
            }
        }

        static void Edit()
        {
            try
            {
                string consoleInput;
                Console.Clear();

                Vehicle vehicle = new Vehicle();
                vehicle.Chassis = new Chassis();

                Console.WriteLine("EDIT A VEHICLE");
                Trace.TraceInformation("User entered the Edit option");

                Console.WriteLine("");
                Console.WriteLine("Please input the Chassis Number:");
                consoleInput = Console.ReadLine();

                while (!uint.TryParse(consoleInput, out uint result))
                {
                    Console.WriteLine("Not a valid number, try again:");

                    consoleInput = Console.ReadLine();
                }
                vehicle = vehicleService.GetByChassis(Convert.ToUInt32(consoleInput));

                if (vehicle == null)
                {
                    Console.WriteLine("There is no Vehicle with such a Chassis Number, please try again");
                    Console.ReadLine();
                    Console.Clear();

                    Menu();
                }
                else
                {

                    Console.WriteLine("");
                    Console.WriteLine("Please input the Vehicle color:");
                    consoleInput = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(consoleInput))
                    {
                        Console.WriteLine("Please input the Vehicle color:");

                        consoleInput = Console.ReadLine();
                    }
                    vehicle.Color = consoleInput;

                    Trace.TraceInformation("User will edit the vehiche:");
                    Trace.TraceInformation("    Chassis Number: {0}", vehicle.Chassis.Number);
                    Trace.TraceInformation("    Chassis Series: {0}", vehicle.Chassis.Series);
                    Trace.TraceInformation("    Type: {0}", vehicle.VehicleType.Name);
                    Trace.TraceInformation("    Color: {0}", vehicle.Color);

                    vehicleService.Put<VehicleValidator>(vehicle);

                    Console.WriteLine("Vehicle edited successfully! Please hit Enter to continue.");
                    Trace.TraceInformation("Vehicle edited successfully");
                    Console.ReadLine();
                    Console.Clear();

                    Menu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error while editing vehicle. Please check log files");
                Trace.TraceInformation("error while editing vehicle: {0}", ex.Message);
                Console.ReadLine();
                Console.Clear();

                Menu();
            }
        }

        static void Delete()
        {
            try
            {
                string consoleInput;
                Console.Clear();

                Vehicle vehicle = new Vehicle();
                vehicle.Chassis = new Chassis();

                Console.WriteLine("DELETE A VEHICLE");
                Trace.TraceInformation("User entered the Delete option");

                Console.WriteLine("");
                Console.WriteLine("Please input the Chassis Number:");
                consoleInput = Console.ReadLine();

                while (!uint.TryParse(consoleInput, out uint result))
                {
                    Console.WriteLine("Not a valid number, try again:");

                    consoleInput = Console.ReadLine();
                }
                vehicle = vehicleService.GetByChassis(Convert.ToUInt32(consoleInput));

                if (vehicle == null)
                {
                    Console.WriteLine("There is no Vehicle with such a Chassis Number, please try again");
                    Console.ReadLine();
                    Console.Clear();

                    Menu();
                }
                else
                {

                    Console.WriteLine("");
                    Console.WriteLine("Are you sure you want to delete this vehicle? input yes or no");
                    consoleInput = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(consoleInput))
                    {
                        Console.WriteLine("Are you sure you want to delete this vehicle? input yes or no");

                        consoleInput = Console.ReadLine();
                    }

                    bool valid = false;
                    bool delete = false;
                    while (!valid)
                    {
                        switch (consoleInput)
                        {
                            case "yes":
                                valid = true;
                                delete = true;
                                break;
                            case "no":
                                valid = true;
                                delete = false;
                                break;
                            default:
                                Console.WriteLine("Are you sure you want to delete this vehicle? input yes or no");
                                consoleInput = Console.ReadLine();
                                break;
                        }
                    }
                    if (delete)
                    {
                        Trace.TraceInformation("User will delete the vehiche:");
                        Trace.TraceInformation("    Chassis Number: {0}", vehicle.Chassis.Number);
                        Trace.TraceInformation("    Chassis Series: {0}", vehicle.Chassis.Series);
                        Trace.TraceInformation("    Type: {0}", vehicle.VehicleType.Name);
                        Trace.TraceInformation("    Color: {0}", vehicle.Color);

                        vehicleService.Delete(vehicle.Id);

                        Console.WriteLine("Vehicle deleted successfully! Please hit Enter to continue.");
                        Trace.TraceInformation("Vehicle deleted successfully");
                        Console.ReadLine();
                        Console.Clear();

                        Menu();
                    }
                    else
                    {
                        Console.Clear();
                        Menu();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error while deleting vehicle. Please check log files");
                Trace.TraceInformation("error while deleting vehicle: {0}", ex.Message);
                Console.ReadLine();
                Console.Clear();

                Menu();
            }
        }

        static void List()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("LIST ALL VEHICLES");
                Console.WriteLine("");
                Trace.TraceInformation("User entered the List All option");

                var fleet = vehicleService.Get();

                Console.WriteLine("CHASSIS NUMBER | CHASSIS SERIES | TYPE | PASSENGERS | COLOR");

                foreach(Vehicle vehicle in fleet)
                {
                    Console.Write(vehicle.Chassis.Number);
                    Console.Write(" | ");
                    Console.Write(vehicle.Chassis.Series);
                    Console.Write(" | ");
                    Console.Write(vehicle.VehicleType.Name);
                    Console.Write(" | ");
                    Console.Write("{0, 0:D3}", vehicle.VehicleType.NumPassengers);
                    Console.Write(" | ");
                    Console.Write(vehicle.Color);
                    Console.WriteLine("");
                }


                Console.WriteLine("");
                Console.WriteLine("Please hit Enter to continue.");
                Console.ReadLine();
                Console.Clear();

                Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error while fatching the vehicles. Please check log files");
                Trace.TraceInformation("error while fatching the vehicles: {0}", ex.Message);
                Console.ReadLine();
                Console.Clear();

                Menu();
            }
        }

        static void Find()
        {
            try
            {
                string consoleInput;
                Console.Clear();

                Vehicle vehicle = new Vehicle();
                vehicle.Chassis = new Chassis();

                Console.WriteLine("FIND A VEHICLE");
                Console.WriteLine("");
                Trace.TraceInformation("User entered the Find option");

                Console.WriteLine("Please input the Chassis Number:");
                consoleInput = Console.ReadLine();

                while (!uint.TryParse(consoleInput, out uint result))
                {
                    Console.WriteLine("Not a valid number, try again:");

                    consoleInput = Console.ReadLine();
                }
                vehicle = vehicleService.GetByChassis(Convert.ToUInt32(consoleInput));

                if (vehicle == null)
                {
                    Console.WriteLine("There is no Vehicle with such a Chassis Number, please try again");
                    Console.ReadLine();
                    Console.Clear();

                    Menu();
                }
                else
                {

                    Console.WriteLine("CHASSIS NUMBER | CHASSIS SERIES | TYPE | PASSENGERS | COLOR");


                    Console.Write(vehicle.Chassis.Number);
                    Console.Write(" | ");
                    Console.Write(vehicle.Chassis.Series);
                    Console.Write(" | ");
                    Console.Write(vehicle.VehicleType.Name);
                    Console.Write(" | ");
                    Console.Write("{0, 0:D3}", vehicle.VehicleType.NumPassengers);
                    Console.Write(" | ");
                    Console.Write(vehicle.Color);
                    Console.WriteLine("");

                    Console.WriteLine("");
                    Console.WriteLine("Please hit Enter to continue.");
                    Console.ReadLine();
                    Console.Clear();

                    Menu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error while fatching the vehicle. Please check log files");
                Trace.TraceInformation("error while fatching the vehicle: {0}", ex.Message);
                Console.ReadLine();
                Console.Clear();

                Menu();
            }
        }

        static void Exit()
        {
            string consoleInput;
            Console.Clear();

            Console.WriteLine("EXIT");
            Console.WriteLine("");
            Trace.TraceInformation("User entered the Exit option");

            Console.WriteLine("Are you sure you want to exit the system? yes or no");
            consoleInput = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(consoleInput))
            {
                Console.WriteLine("Are you sure you want to exit the system? yes or no");

                consoleInput = Console.ReadLine();
            }

            bool valid = false;
            bool exit = false;
            while (!valid)
            {
                switch (consoleInput)
                {
                    case "yes":
                        valid = true;
                        exit = true;
                        break;
                    case "no":
                        valid = true;
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Are you sure you want to exit the system? yes or no");
                        consoleInput = Console.ReadLine();
                        break;
                }
            }
            if(!exit)
            {
                Menu();
            }
            else
            {
                Trace.TraceInformation("User exited the sistem");
            }
        }
    }
    
}
