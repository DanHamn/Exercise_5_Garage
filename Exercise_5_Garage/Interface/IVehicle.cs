using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage
{
    interface IVehicle : IEnumerable
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int NumberOfWheels { get; set; }

        public new IEnumerator GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
    }
}
