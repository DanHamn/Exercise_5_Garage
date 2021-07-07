using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage.Vehicles
{
    public class Airplane : Vehicle
    {
        public double WingSpan { get; set; }
        public int NumberOfEngines { get; set; }

        public Airplane(string registrationNumber, string color, int numberOfWheels, double wingSpan, int numberOfEngines) : 
            base(registrationNumber, color, numberOfWheels)
        {
            WingSpan = wingSpan;
            NumberOfEngines = numberOfEngines;
        }
    }
}
