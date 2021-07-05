using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage
{
    class Garage<T> where T : Vehicle, IEnumerable
    {
    public Vehicle[] vehicles;
        public Garage(int capacity)
        {
            vehicles = new Vehicle[capacity];
        }

        internal Vehicle AddVehicle(Vehicle vehicle, string v1, string v2, int v3)
        {
            vehicle = new Vehicles.Motorcycle(v1, v2, v3,"coal");
            return vehicle;
        }

        public void RemoveVehicle()
        {
            Vehicle.Remove(vehicles,"ABC123");
        }
    }
}
