using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Exercise_5_Garage
{
    class GarageHandler
    {
        static readonly IUI ui = new ConsoleUI();
        private static readonly Random random = new();
        public Garage<Vehicle> Initiate(int size, int filled)
        {
            Garage<Vehicle> garage = new(size);
            if (filled == 0)
            {
                return garage;
            }
            else
            {
                List<Vehicle> listOfVehicles = RandomListOfVehicles(filled);
                foreach (var vehicle in listOfVehicles)
                {
                    garage.Add(vehicle,false);
                }
                return garage;
            }
        }

        private List<Vehicle> RandomListOfVehicles(int filled)
        {
            List<Vehicle> listOfVehicles = new();
            for (int i = 0; i < filled; i++)
            {
                Vehicle vehicle = CreateRandomVehicle();
                listOfVehicles.Add(vehicle);
            }            
            return listOfVehicles;
        }

        public static Vehicle CreateRandomVehicle()
        {
            List<Type> typesList = ListOfVehicles();
            int rnd = random.Next(0, typesList.Count);
            Vehicle vehicle = null;
            if (typesList[rnd].Name == "Motorcycle")
            {
                vehicle = new Vehicles.Motorcycle(RandRegNm(), RandColor(), random.Next(1, 4), RandFuel(null));
            }
            else if (typesList[rnd].Name == "Car")
            {
                vehicle = new Vehicles.Car(RandRegNm(), RandColor(), random.Next(2, 4), RandFuel(null), random.Next(2, 3));
            }
            else if (typesList[rnd].Name == "Bus")
            {
                vehicle = new Vehicles.Bus(RandRegNm(), RandColor(), random.Next(2, 6), RandFuel(null), random.Next(2, 3), random.Next(5, 20));
            }
            else if (typesList[rnd].Name == "Boat")
            {
                var boat = RandBoat();
                vehicle = new Vehicles.Boat(RandRegNm(), RandColor(), 0, boat, RandFuel(boat), random.Next(5, 20));
            }
            else if (typesList[rnd].Name == "Airplane")
            {
                var boat = RandBoat();
                vehicle = new Vehicles.Airplane(RandRegNm(), RandColor(), random.Next(2, 6), random.Next(20, 100), random.Next(0, 6));
            }

            return vehicle;
        }

        private static List<Type> ListOfVehicles()
        {
            var types = TypesOfVehicles();
            List<Type> typesList = new();
            foreach (var type in types)
            {
                typesList.Add(type);
            }

            return typesList;
        }

        private static string RandRegNm()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string chars = new(Enumerable.Repeat(characters, 3)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
            int value = random.Next(1000);

            return new string(chars + value.ToString("000"));
        }
        private static string RandColor()
        {
            string[] colors = new string[] { "Red", "Blue", "Gray", "Black", "Yellow" };
            return colors[random.Next(colors.Length)];
        }
        private static string RandFuel(string boat)
        {
            if (boat == "sailing")
            {
                return "Wind";
            }
            else
            {
                string[] fuel = new string[] { "Gasoline", "Diesel" };
                return fuel[random.Next(fuel.Length)];
            }
        }
        private static string RandBoat()
        {
            string[] boat = new string[] { "Sailing", "Motor" };
            return boat[random.Next(boat.Length)];
        }

        private static IEnumerable<Type> TypesOfVehicles()
        {
            var assembly = typeof(Vehicle).Assembly;
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Vehicle)));
            return types;
        }

        internal void AddMotorcycle(Garage<Vehicle> garage, string v1, string v2, int v3, string v4)
        {
            garage.Add(new Vehicles.Motorcycle(v1, v2, v3, v4),true);
        }
        internal void AddCar(Garage<Vehicle> garage, string v1, string v2, int v3, string v4, int v5)
        {
            garage.Add(new Vehicles.Car(v1, v2, v3, v4, v5),true);
        }

        public static void RemoveVehicle(Garage<Vehicle> garage, string v1)
        {
            garage.Remove(garage, v1);
        }

        internal static Vehicle CreateVehicle()
        {
            throw new NotImplementedException();
        }

        internal static int NumberOf(Garage<Vehicle> garage, Type type)
        {
            return garage.NumberOf(garage, type);
        }


        internal static string GetRegNummer(Vehicle item)
        {
            return item.RegistrationNumber;
        }
    }
}
