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
            //string input = ui.Input();
            //_ = int.TryParse(input, out int size);
            SeedData(5);
            ListGarage();
            ListType();
            garageHandler.RemoveVehicle(garage, "abc124");

            ListGarage();
            garageHandler.AddCar(garage, "ABC224", "red", 4, "desiel", 5);
            ListGarage();
        }

        private static void ListType()//Finds all subclasses of vehicle and list the number of them in the garage
        {
            var assembly = typeof(Vehicle).Assembly;
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Vehicle)));
            foreach (var type in types)
            {
                ui.Print($"The number of {type.Name}: {GarageHandler.NumberOf(garage, type)}");
            }
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
            garageHandler.AddCar(garage, "ABC124", "red", 4, "desiel", 5);
            garageHandler.AddCar(garage, "ABC125", "red", 4, "desiel", 5);
            garageHandler.AddCar(garage, "ABC126", "red", 4, "desiel", 5);
            //garageHandler.AddCar(garage, "ABC127", "red", 4, "desiel", 5);
            //garageHandler.AddCar(garage, "ABC128", "red", 4, "desiel", 5);
        }
    }
}
