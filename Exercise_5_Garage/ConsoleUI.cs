﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5_Garage
{
    public class ConsoleUI : IUI
    {
        public void Print(string massage)
        {
            Console.WriteLine(massage);
        }
    }
}
