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

        public void AddVehicle()
        {
            vehicles.Add(new Vehicle());
        }
        public void RemoveVehicle()
        {
            vehicles.Remove("ABC123");
        }
    }
}
