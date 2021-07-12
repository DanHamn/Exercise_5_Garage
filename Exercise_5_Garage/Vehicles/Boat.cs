using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage.vehicles
{
    class Boat : Vehicle
    {
        public string BoatType { get; set; }
        public string FuelType { get; set; }
        public double Length { get; set; }

        public Boat(string registrationNumber, string color, int numberOfWheels, string boatType, string fuelType, double length) : 
            base(registrationNumber, color, numberOfWheels)
        {
            BoatType = boatType;
            FuelType = fuelType;
            Length = length;
        }
    }
}
