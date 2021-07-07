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
                    ui.Print($"Can not add {vehicle} with req number {vehicle.RegistrationNumber}.");
                    ui.Print($"The Garage is full.");
                }
            }
        }

        internal int NumberOf(Garage<Vehicle> garage, Type type)
        {
            return garage.vehicles.Count(s => s != null && s.GetType() == type);
        }

        internal void NumberOf(bool v, Type type1, Type type2)
        {
            throw new NotImplementedException();
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
