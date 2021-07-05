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
        internal void Initiate(int size)
        {
            Garage<Vehicle> garage = new(size);


            garage.vehicles[0]=garage.AddVehicle(garage.vehicles[0], "123abc", "red", 4);
        }
    }
}
