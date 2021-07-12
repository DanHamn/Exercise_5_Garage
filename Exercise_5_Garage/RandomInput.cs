using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage
{
    class RandomInput
    {
        private static readonly Random random = new();
        public static string RandRegNm()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string chars = new(Enumerable.Repeat(characters, 3)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
            int value = random.Next(1000);

            return new string(chars + value.ToString("000"));
        }
        public static string RandColor()
        {
            string[] colors = new string[] { "Red", "Blue", "Gray", "Black", "Yellow" };
            return colors[random.Next(colors.Length)];
        }
        public static string RandFuel(string boat)
        {
            if (boat == "sailing")
            {
                return "Wind";
            }
            else
            {
                string[] fuel = new string[] { "Gasoline", "Diesel" };
                return fuel[random.Next(fuel.Length)];
            }
        }
        public static string RandBoat()
        {
            string[] boat = new string[] { "Sailing", "Motor" };
            return boat[random.Next(boat.Length)];
        }
    }
}
