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
        public Garage<Vehicle> Initiate(int size)
        {
            Garage<Vehicle> garage = new(size);
            return garage;
        }
        internal void AddMotorcycle(Garage<Vehicle> garage, string v1, string v2, int v3, string v4)
        {
            garage.Add(new Vehicles.Motorcycle(v1, v2, v3, v4));
        }

        public void RemoveVehicle(Garage<Vehicle> garage, string v1)
        {
            garage.Remove(garage,v1);
        }

        internal static int NumberOf(Garage<Vehicle> garage, Type type)
        {
            return garage.NumberOf(garage, type);
        }

        internal void AddCar(Garage<Vehicle> garage, string v1, string v2, int v3, string v4, int v5)
        {
            garage.Add(new Vehicles.Car(v1, v2, v3, v4, v5));
        }

        internal static string GetRegNummer(Vehicle item)
        {
            return item.RegistrationNumber;
        }



        //    //garage.vehicles[0]=garage.AddVehicle(garage.vehicles[0], "123abc", "red", 4);
        //}
        //internal Vehicle AddVehicle(Garage<Vehicle> garage, string v1, string v2, int v3)
        //{
        //    foreach (Vehicle vehicle in garage.vehicles)
        //    {
        //        vehicle.Add(new Vehicles.Motorcycle(v1, v2, v3, "coal"));

        //    }

        //    return vehicle;
        //}
    }
}
