using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Exercise_5_Garage
{

    class Manager
    {
        static readonly IUI ui = new ConsoleUI();
        public static IHandler handler = new GarageHandler();
        public static Garage<Vehicle> garage;
        public static void Start()
        {

            ui.Print("Welcome to the garage manager program" +
                "\nLets Start by building a garage.");
            CreateGarage();
            ui.Print("What would you like to do next?");
            MenuText();
            bool exit = false;
            while (exit != true)
            {
                ui.Print("Please enter input." +
                    "\n7. Bring up menu navigation again.");
                switch (ui.Input()[0])
                {
                    case '1':
                        CreateGarage();
                        ListGarage();
                        break;
                    case '2':
                        AddOrRemove();
                        break;
                    case '3':
                        ListGarage();
                        break;
                    case '4':
                        ListType();
                        break;
                    case '5':
                        SearchWithRegNumber();
                        break;
                    case '6':
                        SearchAfterCharacteristics();
                        break;
                    case '7':
                        MenuText();
                        break;
                    case '0':
                        exit = true;
                        break;
                    default:
                        ui.Print("Please enter some valid input (1, 2, 3 ,4, 5, 6, 0)");
                        break;
                }
            }
        }
        private static void SearchWithRegNumber()
        {
            ui.Print("Please input the registration number for the vehicle you want to search for:");
            string regNum = ui.Input();
            (int i, bool exists, string type) = handler.RegNumberSearch(garage, regNum);
            if (exists == true)
            {
                ui.Print($"The {type} with registration number is in the garage on spot number {i}");
            }
            else if (exists == false)
            {
                ui.Print($"There was no vehicle with registration number {regNum} in the garage");
            }
        }

        private static void SearchAfterCharacteristics()
        {
            List<string> propertyList = new();
            Type[] types = typeof(Vehicle).Assembly.GetTypes();
            foreach (var type in types)
            {
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    propertyList.Add(prop.Name);
                    propertyList = propertyList.Distinct().ToList();
                }
            }
            bool exit = false;
            while (exit != true)
            {
                ui.Print("What do you want to do?" +
                    "\n1. Add search term to list" +
                    "\n2. Search with the list of terms specified" +
                    "\n3. Clear list of previous specified search terms" +
                    "\n0. Exit to main menu.");

                string searchProp = null;
                string searchTerm = null;
                switch (ui.Input()[0])
                {
                    case '1':
                        bool done = false;
                        while (done != true)
                        {
                            ui.Print("What characterestic to add to the search list?" +
                                "\n The alternativs are:");
                            foreach (var prop in propertyList)
                            {
                                ui.Print("\t*" + prop);
                            }

                            searchProp = ui.Input();
                            foreach (var prop2 in propertyList)
                            {
                                if (!prop2.ToLower().Contains(searchProp.ToLower()))
                                {
                                    ui.Print("Please enter a characterestic from the list of alternatives.");
                                }
                                else
                                {
                                    ui.Print("Please enter the term you want to search for");
                                    searchTerm = ui.Input();
                                    done = true;
                                }
                            }

                        }
                        break;
                    case '2':
                            int number = handler.CharacteristicsSearch(garage, searchProp, searchTerm);
                        ui.Print("");
                        break;
                    case '3':
                        break;
                    default:
                        break;
                }
            }
        }


        private static void MenuText()
        {
            ui.Print("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 5, 6, 0) of your choice"
                + "\n1. Create a new garage; either an empty one or one populated with random vehicles."
                + "\n2. Add or remove a vehicle from the garage."
                + "\n3. Show the full list of the garage."
                + "\n4. Show the number of specific vehicles in the garage."
                + "\n5. Search for a vehicle based on registration number."
                + "\n6. Search for vehicles based on characteristics."
                + "\n0. Exit the application");
        }
        private static void CreateGarage()
        {
            ui.Print("What is the max capacity of the garage?");
            int size = LowerThenIntCheck(0);

            ui.Print("How many vehicles are inside the garage from the start?");
            int filled = LowerThenIntCheck(0);
            bool full;
            (garage, full) = handler.Initiate(size, filled);
            if (full)
            {
                ui.Print("Could not fill the garage with the requested amount of vehicles." +
                    "\nThe capacity of the garage is too small.");
            }
        }
        private static void AddOrRemove()
        {
            bool exit = false;
            while (exit != true)
            {
                ListGarage();

                ui.Print("What do you want to do: " +
                "\n1. Add a new vehicle to the garage" +
                "\n2. Remove an existing vehicle from the garage" +
                "\n0. Exit to main menu");
                switch (ui.Input()[0])
                {
                    case '1':
                        ui.Print("1. Add a random vehicle" +
                            "\n2. Add a specific vehicle");
                        switch (ui.Input()[0])
                        {
                            case '1':
                                garage.Add(handler.CreateRandomVehicle(), true);
                                break;
                            case '2':
                                garage.Add(CreateSpecificVehicle(), true);
                                break;
                            default:
                                ui.Print("Please enter some valid input (1, 2, 0)");
                                break;
                        }
                        break;
                    case '2':
                        ui.Print("Enter the registration number for the vehicle you want to remove.");
                        string input = ui.Input();
                        handler.RemoveVehicle(garage, input);
                        break;
                    case '0':
                        exit = true;
                        break;
                    default:
                        ui.Print("Please enter some valid input (1, 2, 0)");
                        break;
                }

            }
        }

        private static Vehicle CreateSpecificVehicle()
        {
            Vehicle vehicle = null;
            List<object> specifics = new();

            string type = BaseProp(specifics);

            if (type == "Motorcycle")
            {
                vehicle = CreateMotorcycle(specifics, type);
            }
            else if (type == "Car")
            {
                vehicle = CreateCar(specifics, type);
            }
            else if (type == "Bus")
            {
                vehicle = CreateBus(specifics, type);
            }
            else if (type == "Boat")
            {
                vehicle = CreateBoat(specifics, type);
            }
            else if (type == "Airplane")
            {
                vehicle = CreateAirplane(specifics, type);
            }
            else
            {
                ui.Print($"{type} was not a valid type");
            }
            return vehicle;
        }

        private static Vehicle CreateAirplane(List<object> specifics, string type)
        {
            Vehicle vehicle;
            ui.Print($"How big is the wingspan of the {type} in meters?");
            double wingspan = LowerThenDoubleCheck(0);
            specifics.Add(wingspan);
            ui.Print($"How many engines does the {type} have?");
            int engines = LowerThenIntCheck(0);
            specifics.Add(engines);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static Vehicle CreateBoat(List<object> specifics, string type)
        {
            Vehicle vehicle;
            ui.Print($"What kind of {type} is it?");
            string boatType = ui.Input();
            specifics.Add(boatType);
            ui.Print($"What type of fuel is the {type} using?");
            string fuelType = ui.Input();
            specifics.Add(fuelType);
            ui.Print($"How long is the {type} in meters?");
            double length = LowerThenDoubleCheck(0);
            specifics.Add(length);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static Vehicle CreateBus(List<object> specifics, string type)
        {
            Vehicle vehicle;
            ui.Print($"What type of fuel is the {type} using?");
            string fuelType = ui.Input();
            specifics.Add(fuelType);
            ui.Print($"How manu seats are inside the {type}?");
            int numberOfSeats = LowerThenIntCheck(1);
            specifics.Add(numberOfSeats);
            ui.Print($"How long is the {type} in meters?");
            double length = LowerThenDoubleCheck(0);
            specifics.Add(length);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static Vehicle CreateCar(List<object> specifics, string type)
        {
            Vehicle vehicle;
            ui.Print($"What type of fuel is the {type} using?");
            string fuelType = ui.Input();
            specifics.Add(fuelType);
            ui.Print($"How manu seats are inside the {type}?");
            int numberOfSeats = LowerThenIntCheck(1);
            specifics.Add(numberOfSeats);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static Vehicle CreateMotorcycle(List<object> specifics, string type)
        {
            Vehicle vehicle;
            ui.Print($"What type of fuel is the {type} using?");
            string fuelType = ui.Input();
            specifics.Add(fuelType);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static string BaseProp(List<object> specifics)
        {
            ui.Print("What type of vehicle do you want to add?");
            string type = ui.Input();
            specifics.Add(type);

            ui.Print($"What is the registration number for the {type}?");
            string registrationNumber = ui.Input();
            specifics.Add(registrationNumber);

            ui.Print($"What color is the {type}?");
            string color = ui.Input();
            specifics.Add(color);

            ui.Print($"What is the number of wheels on the {type}?");
            int numberOfWheels = LowerThenIntCheck(1);
            specifics.Add(numberOfWheels);
            return type;
        }

        private static double LowerThenDoubleCheck(int lowestDouble)
        {
            double integer = new();
            bool done = false;
            while (done != true)
            {
                done = double.TryParse(ui.Input(), out integer);
                if (done != true && integer <= lowestDouble)
                {
                    ui.Print($"Have input a value greater then {lowestDouble}.");
                    done = false;
                }
            }
            return integer;
        }

        private static int LowerThenIntCheck(int lowestInt)
        {
            int integer = new();
            bool done = false;
            while (done != true)
            {
                done = int.TryParse(ui.Input(), out integer);
                if (done != true && integer < lowestInt)
                {
                    ui.Print($"Have to input an integer bigger then {lowestInt}.");
                    done = false;
                }
            }
            return integer;
        }

        private static void ListGarage()
        {
            int i = 1;
            ui.Print("");
            ui.Print("Right now the Garage contains:");
            foreach (Vehicle vehicle in garage)
            {
                if (vehicle != null)
                {
                    ui.Print($"|{i}|: {vehicle} with req nummer {handler.GetRegNummer(vehicle)}");
                }
                else if (vehicle == null)
                {
                    ui.Print($"|{i}|:");
                }
                i++;
            }
            ui.Print("");
        }

        private static void ListType()//Finds all subclasses of Vehicle and list the number of them in the garage
        {
            var types = handler.TypesOfVehicles();
            ui.Print("");
            foreach (var type in types)
            {
                ui.Print($"The number of {type.Name}: {handler.NumberOf(garage, type)}");
            }
            ui.Print("");
        }
    }
}
