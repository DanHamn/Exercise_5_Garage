using System;
using System.Linq;


namespace Exercise_5_Garage
{
    class Program
    {
        static readonly IUI ui = new ConsoleUI();
        static GarageHandler garageHandler = new();
        private static Garage<Vehicle> garage;

        static void Main()
        {
            ui.Print("Welcome to a new Garage");
            ui.Print("How big is the garage?");
            string input = ui.Input();
            _ = int.TryParse(input, out int size);
            SeedData(5);
            ListGarage();
            ListType();

        }

        private static void ListType()
        {
            ui.Print($"number of Motorcycles:{GarageHandler.NumberOf(garage, typeof(Vehicles.Motorcycle))}");
            ui.Print($"number of Cars:{GarageHandler.NumberOf(garage, typeof(Vehicles.Car))}");
            ui.Print($"number of Buses:{GarageHandler.NumberOf(garage, typeof(Vehicles.Bus))}");
            ui.Print($"number of Boats:{GarageHandler.NumberOf(garage, typeof(Vehicles.Boat))}");
            ui.Print($"number of Airplanes:{GarageHandler.NumberOf(garage, typeof(Vehicles.Airplane))}");
        }

        private static void ListGarage()
        {
            int i = 1;
            ui.Print("Right now the Garage contains:");
            foreach (Vehicle item in garage)
            {
                if (item != null)
                {
                    ui.Print($"|{i}|: {item} with req nummer {GarageHandler.GetRegNummer(item)}");
                }
                else if (item == null)
                {
                    ui.Print($"|{i}|:");
                }
                i++;
            }
        }

        private static void SeedData(int size)
        {
            garage = garageHandler.Initiate(size);
            garageHandler.AddMotorcycle(garage, "ABC123", "red", 4, "oil");
            garageHandler.AddCar(garage, "ABC123", "red", 4, "desiel", 5);
            garageHandler.AddCar(garage, "ABC123", "red", 4, "desiel", 5);
            garageHandler.AddCar(garage, "ABC123", "red", 4, "desiel", 5);
            //garageHandler.AddCar(garage, "ABC123", "red", 4, "desiel", 5);
            //garageHandler.AddCar(garage, "ABC123", "red", 4, "desiel", 5);
        }
    }
}
