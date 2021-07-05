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
            string size = ui.Input();
            ui.Print(size);
            //garageHandler.Initiate();
        }
    }
}
