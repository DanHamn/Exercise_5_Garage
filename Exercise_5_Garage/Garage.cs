using Exercise_5_Garage.Vehicles;
using System;
using System.Collections;
using System.Linq;

namespace Exercise_5_Garage
{
    class Garage<T> where T : Vehicle, IEnumerable

    {
        private readonly Vehicle[] vehicles;

        public Garage(int capacity)
        {
            vehicles = new Vehicle[capacity];
        }


        internal void Add(Vehicle vehicle)
        {
            IUI ui = new ConsoleUI();
            foreach (Vehicle ve in vehicles)
            {
                if (ve != null && ve.RegistrationNumber == vehicle.RegistrationNumber)
                {
                    ui.Print($"Cannot add {vehicle} with reg. number {vehicle.RegistrationNumber}");
                    ui.Print("There is already a vehicle in the garage with that reg. number");
                    return;
                }
            }
            FirstEmpty(vehicle);
        }

        private void FirstEmpty(Vehicle vehicle)
        {
            IUI ui = new ConsoleUI();
            int i = 0;
            foreach (Vehicle ve in vehicles)
            {
                if (ve == null)
                {
                    vehicles[i] = vehicle;
                    break;
                }
                i++;
                if (i == vehicles.Length)
                {
                    ui.Print($"Can not add {vehicle} with reg. number {vehicle.RegistrationNumber}.");
                    ui.Print($"The Garage is full.");
                }
            }
        }

        internal void Remove(Garage<Vehicle> garage, string v1)
        {
            for (int i = 0; i < garage.vehicles.Length; i++)
            {
                if (vehicles[i] != null && vehicles[i].RegistrationNumber.ToLower() == v1.ToLower())
                {
                    vehicles[i] = null;
                }
            }
        }

        internal int NumberOf(Garage<Vehicle> garage, Type type)
        {
            return garage.vehicles.Count(s => s != null && s.GetType() == type);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in vehicles)
            {
                yield return item;
            }
        }
    }
}
