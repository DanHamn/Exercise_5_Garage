﻿using Exercise_5_Garage.vehicles;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Exercise_5_Garage
{
    class Garage<T> where T : IVehicle, IEnumerable

    {
        private readonly IVehicle[] vehicles;

        public Garage(int capacity)
        {
            vehicles = new IVehicle[capacity];
        }


        internal void Add(IVehicle vehicle, bool v)
        {
            IUI ui = new ConsoleUI();
            foreach (IVehicle ve in vehicles)
            {
                if (ve != null && ve.RegistrationNumber == vehicle.RegistrationNumber)
                {
                    ui.Print($"Cannot add {vehicle} with reg. number {vehicle.RegistrationNumber}");
                    ui.Print("There is already a vehicle in the garage with that reg. number");
                    return;
                }
            }
            FirstEmpty(vehicle, v);
        }

        private void FirstEmpty(IVehicle vehicle, bool v)
        {
            IUI ui = new ConsoleUI();
            int i = 0;
            foreach (IVehicle ve in vehicles)
            {
                if (ve == null)
                {
                    vehicles[i] = vehicle;
                    if (v)
                    {
                        ui.Print($"The {vehicle} with reg. number {vehicle.RegistrationNumber} was parked");
                    }
                    break;
                }
                i++;
                if (i == vehicles.Length && v)
                {
                    ui.Print($"Can not add {vehicle} with reg. number {vehicle.RegistrationNumber}.");
                    ui.Print($"The Garage is full.");
                }
            }
        }

        internal void Remove(Garage<IVehicle> garage, string v1)
        {
            for (int i = 0; i < garage.vehicles.Length; i++)
            {
                if (vehicles[i] != null && vehicles[i].RegistrationNumber.ToLower() == v1.ToLower())
                {
                    vehicles[i] = null;
                }
            }
        }


        internal int NumberOf(Garage<IVehicle> garage, Type type)
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
