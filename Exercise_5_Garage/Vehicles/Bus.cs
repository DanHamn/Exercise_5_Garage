using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage.Vehicles
{
    class Bus : Vehicle
    {
        public string FuelType { get; set; }
        public object NumberOfSeats { get; set; }
        public double Lenght { get; set; }

        public Bus(string registrationNumber, string color, int numberOfWheels, string fuelType, int numberOfSeats, double lenght) : 
            base(registrationNumber, color, numberOfWheels)
        {
            FuelType = fuelType;
            NumberOfSeats = numberOfSeats;
            Lenght = lenght;
        }
    }
}
