using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage.vehicles
{
    class Bus : Vehicle
    {
        public string FuelType { get; set; }
        public object NumberOfSeats { get; set; }
        public double Length { get; set; }

        public Bus(string registrationNumber, string color, int numberOfWheels, string fuelType, int numberOfSeats, double length) : 
            base(registrationNumber, color, numberOfWheels)
        {
            FuelType = fuelType;
            NumberOfSeats = numberOfSeats;
            Length = length;
        }
    }
}
