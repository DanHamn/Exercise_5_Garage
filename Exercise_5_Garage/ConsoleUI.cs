using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage
{
    public class ConsoleUI : IUI
    {
        public string Input()
        {
            string input = "";
            while (true)
            {
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine("Please make an input.");
                }
                else
                {
                    break;
                }

            }
            return input;
        }

        public void Print(string massage)
        {
            Console.WriteLine(massage);
        }
    }
}
