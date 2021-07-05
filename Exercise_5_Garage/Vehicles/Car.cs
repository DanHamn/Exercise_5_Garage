using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage.Vehicles
{
    class Car : Vehicle
    {
        public string FuelType { get; set; }
        public object NumberOfSeats { get; set; }

        public Car(string registrationNumber, string color, int numberOfWheels, string fuelType, int numberOfSeats) :
            base(registrationNumber, color, numberOfWheels)
        {
            FuelType = fuelType;
            NumberOfSeats = numberOfSeats;
        }
    }
}
