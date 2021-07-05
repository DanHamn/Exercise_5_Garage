using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage.Vehicles
{
    class Boat : Vehicle
    {
        public string BoatType { get; set; }
        public string FuelType { get; set; }
        public double Lenght { get; set; }

        public Boat(string registrationNumber, string color, int numberOfWheels, string boatType, string fuelType, double lenght) : 
            base(registrationNumber, color, numberOfWheels)
        {
            FuelType = fuelType;
            Lenght = lenght;
            BoatType = boatType;
        }
    }
}
