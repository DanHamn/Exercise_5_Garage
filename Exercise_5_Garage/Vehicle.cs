using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage
{
    public abstract class Vehicle : IEnumerable
    {
        public IEnumerable<object> vehicles;

        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int NumberOfWheels { get; set; }

        public Vehicle(string registrationNumber, string color, int numberOfWheels)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            NumberOfWheels = numberOfWheels;
        }
        public IEnumerator GetEnumerator()
        {
            foreach (var item in vehicles)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public override string ToString()
        {
            return GetType().Name;
        }


    }
}
