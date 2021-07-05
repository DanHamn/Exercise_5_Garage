using System;


namespace Exercise_5_Garage
{
    class Program
    {
        static readonly IUI ui = new ConsoleUI();
        static GarageHandler garageHandler = new();
        static void Main()
        {
            ui.Print("Welcome to a new Garage");
            ui.Print("How big is the garage?");
            string input = ui.Input();
            _ = int.TryParse(input, out int size);
            garageHandler.Initiate(size);

        }
    }
}
