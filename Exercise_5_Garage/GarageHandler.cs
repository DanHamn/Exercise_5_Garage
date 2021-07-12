using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Exercise_5_Garage
{
    class GarageHandler : IHandler
    {
        private static readonly Random random = new();
        (Garage<Vehicle>, bool) IHandler.Initiate(int size, int numberOfVehicles)
        {
            bool full = false;
            Garage<Vehicle> garage = new(size);
            if (numberOfVehicles == 0)
            {
                return (garage, full);
            }
            else
            {
                for (int i = 0; i < numberOfVehicles; i++)
                {
                    Vehicle vehicle = CreateRandomVehicle();
                    garage.Add(vehicle, false);
                    if (numberOfVehicles > size)
                    {
                        full = true;
                    }
                }
                return (garage, full);
            }
        }
        public Vehicle CreateRandomVehicle()
        {
            List<Type> typesList = TypesOfVehicles().ToList();
            int rnd = random.Next(0, typesList.Count);
            Vehicle vehicle = null;
            if (typesList[rnd].Name == "Motorcycle")
            {
                vehicle = new vehicles.Motorcycle(RandomInput.RandRegNm(), RandomInput.RandColor(), random.Next(1, 4), RandomInput.RandFuel(null));
            }
            else if (typesList[rnd].Name == "Car")
            {
                vehicle = new vehicles.Car(RandomInput.RandRegNm(), RandomInput.RandColor(), random.Next(2, 4), RandomInput.RandFuel(null), random.Next(2, 3));
            }
            else if (typesList[rnd].Name == "Bus")
            {
                vehicle = new vehicles.Bus(RandomInput.RandRegNm(), RandomInput.RandColor(), random.Next(2, 6), RandomInput.RandFuel(null), random.Next(2, 3), random.Next(5, 20));
            }
            else if (typesList[rnd].Name == "Boat")
            {
                var boat = RandomInput.RandBoat();
                vehicle = new vehicles.Boat(RandomInput.RandRegNm(), RandomInput.RandColor(), 0, boat, RandomInput.RandFuel(boat), random.Next(5, 20));
            }
            else if (typesList[rnd].Name == "Airplane")
            {
                vehicle = new vehicles.Airplane(RandomInput.RandRegNm(), RandomInput.RandColor(), random.Next(2, 6), random.Next(20, 100), random.Next(0, 6));
            }
            return vehicle;
        }
        public IEnumerable<Type> TypesOfVehicles()
        {
            var types = typeof(Vehicle)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Vehicle)));
            return types;
        }

        Vehicle IHandler.CreateVehicle(List<object> specifics)
        {
            Vehicle vehicle = null;
            if ((string)specifics[0] == "Motorcycle")
            {
                vehicle = new vehicles.Motorcycle((string)specifics[1], (string)specifics[2], (int)specifics[3], (string)specifics[4]);
            }
            else if ((string)specifics[0] == "Car")
            {
                vehicle = new vehicles.Car((string)specifics[1], (string)specifics[2], (int)specifics[3], (string)specifics[4], (int)specifics[5]);
            }
            else if ((string)specifics[0] == "Bus")
            {
                vehicle = new vehicles.Bus((string)specifics[1], (string)specifics[2], (int)specifics[3], (string)specifics[4], (int)specifics[5], (double)specifics[6]);
            }
            else if ((string)specifics[0] == "Boat")
            {
                vehicle = new vehicles.Boat((string)specifics[1], (string)specifics[2], (int)specifics[3], (string)specifics[4], (string)specifics[5], (double)specifics[6]);
            }
            else if ((string)specifics[0] == "Airplane")
            {
                vehicle = new vehicles.Airplane((string)specifics[1], (string)specifics[2], (int)specifics[3], (double)specifics[4], (int)specifics[5]);
            }
            return vehicle;
        }

        void IHandler.RemoveVehicle(Garage<Vehicle> garage, string v1)
        {
            garage.Remove(garage, v1);
        }

        int IHandler.NumberOf(Garage<Vehicle> garage, Type type)
        {
            return garage.NumberOf(garage, type);
        }

        string IHandler.GetRegNummer(Vehicle item)
        {
            return item.RegistrationNumber;
        }

        (int, bool, string) IHandler.RegNumberSearch(Garage<Vehicle> garage, string regNum)
        {
            int i = 0;
            foreach (Vehicle vehicle in garage)
            {
                if (vehicle.RegistrationNumber == regNum)
                {
                    return (i, true, vehicle.GetType().Name);
                }
                i++;
            }
            return (i, false, null);
        }

        int IHandler.CharacteristicsSearch(Garage<Vehicle> garage, string searchProp, string searchTerm)
        {
            int i = 0;
            foreach (Vehicle vehicle in garage)
            {
                var properties = vehicle.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    if (prop.Name.ToLower() == searchProp.ToLower())
                    {
                        if (prop.GetValue(prop.Name).ToString().ToLower() == searchTerm.ToLower())
                        {
                            i++;
                        }
                    }

                }
            }
            return i;
        }
    }
}
