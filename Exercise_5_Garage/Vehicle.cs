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
        private string registrationNumber;
        public string color;
        public int numberOfWheels;

        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set { registrationNumber = value.ToUpper(); }
        }
        public string Color
        {
            get { return color; }
            set
            {
                string temp = value.ToLower();
                color = char.ToUpper(temp[0]) + temp[1..];
            }
        }
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
