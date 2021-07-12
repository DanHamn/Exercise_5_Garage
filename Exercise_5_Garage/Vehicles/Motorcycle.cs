using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage.vehicles
{
    class Motorcycle : Vehicle
    {
        public string FuelType { get; set; }
        public Motorcycle(string registrationNumber, string color, int numberOfWheels, string fuelType) : 
            base(registrationNumber, color, numberOfWheels)
        {
            FuelType = fuelType;
        }
    }
}
