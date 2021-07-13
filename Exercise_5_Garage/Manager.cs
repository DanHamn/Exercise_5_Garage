using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Exercise_5_Garage
{

    class Manager
    {
        static readonly IUI ui = new ConsoleUI();
        public static IHandler handler = new GarageHandler();
        public static Garage<IVehicle> garage;
        public static void Start()
        {

            ui.Print("Welcome to the garage manager program" +
                "\nLets Start by building a garage.");
            CreateGarage();
            ListGarage();
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
            string regNum = RegNumInput();
            (int i, bool exists, string type) = handler.RegNumberSearch(garage, regNum);
            if (exists == true)
            {
                ui.Print($"The {type} with registration number {regNum} is in the garage on spot number {i + 1}");
            }
            else if (exists == false)
            {
                ui.Print($"There was no vehicle with registration number {regNum} in the garage");
            }
        }

        private static void SearchAfterCharacteristics()
        {
            List<string> propertyList = new();
            var types = typeof(IVehicle).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Vehicle)));
            foreach (var type in types)
            {
                propertyList.Add(type.Name);
            }
            foreach (var type in types)
            {
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    propertyList.Add(prop.Name);
                    propertyList = propertyList.Distinct().ToList();
                }
            }
            bool exit = false;
            List<string> searchPropList = new();
            List<string> searchTermList = new();
            while (exit != true)
            {
                ui.Print("What do you want to do?" +
                    "\n1. Add search term to list" +
                    "\n2. Search with the list of terms specified" +
                    "\n3. Clear list of previous specified search terms" +
                    "\n0. Exit to main menu.");

                switch (ui.Input()[0])
                {
                    case '1':
                        string searchProp = null;
                        string searchTerm = null;
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
                            foreach (var prop in propertyList)
                            {
                                if (prop.ToLower() == searchProp.ToLower())
                                {
                                    for (int i = 0; i < types.Count(); i++)
                                    {
                                        if (prop == propertyList[i])
                                        {
                                            searchTerm = "";
                                            done = true;
                                            break;
                                        }
                                    }
                                    if (searchTerm == null)
                                    {
                                        ui.Print("Please enter the term you want to search for");
                                        searchTerm = ui.Input();
                                        done = true;
                                        break;
                                    }
                                }
                                if (searchTerm != null)
                                {
                                    break;
                                }
                            }
                            if (searchTerm == null)
                            {
                                ui.Print("Please enter a characterestic from the list of alternatives.");
                                done = false;
                            }

                        }
                        searchPropList.Add(searchProp);
                        searchTermList.Add(searchTerm);
                        break;
                    case '2':
                        List<IVehicle> vehicles;
                        vehicles = handler.CharacteristicsSearch(garage, searchPropList, searchTermList);
                        if (vehicles.Count == 0)
                        {
                            ui.Print("There was no vehicles with the characteristics you searched for.");
                        }
                        else
                        {
                            List<string> strOutput = new();
                            for (int i = 0; i < searchPropList.Count; i++)
                            {
                                if (string.IsNullOrEmpty(searchTermList[i]))
                                {
                                    strOutput.Add($"{searchPropList[i]}");
                                }
                                else
                                {
                                    strOutput.Add($"{searchTermList[i]}");

                                }
                            }
                            ui.Print(string.Format("These are vehicles that are " +
                                "\n{0}.", string.Join(", ", strOutput)));

                            for (int i = 0; i < vehicles.Count; i++)
                            {
                                ui.Print($"{vehicles[i]} with req nummer {handler.GetRegNummer(vehicles[i])}");
                            }
                        }
                        break;
                    case '3':
                        searchPropList.Clear();
                        searchTermList.Clear();
                        break;
                    case '0':
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }


        private static void MenuText()
        {
            ui.Print("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 5, 6, 7, 0) of your choice"
                + "\n1. Create a new garage."
                + "\n2. Add or remove a vehicle from the current garage."
                + "\n3. Show the list of the garage."
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
                        bool exit1 = false;
                        while (exit1 != true)
                        {
                            ui.Print("1. Add a random vehicle" +
                                "\n2. Add a specific vehicle" +
                                "\n0. Exit to previous menu");
                            switch (ui.Input()[0])
                            {
                                case '1':
                                    handler.AddVehicle(garage, handler.CreateRandomVehicle(), true);
                                    break;
                                case '2':
                                    handler.AddVehicle(garage, CreateSpecificVehicle(), true);
                                    break;
                                case '0':
                                    exit1 = true;
                                    break;
                                default:
                                    ui.Print("Please enter some valid input (1, 2, 0)");
                                    break;
                            }
                        }
                        break;
                    case '2':
                        ui.Print("Enter the registration number for the vehicle you want to remove.");
                        string input = RegNumInput();
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

        private static string RegNumInput()
        {
            bool done = false;
            string input = new("");
            while (done != true)
            {
                string v = ui.Input();
                if (v.Length == 6)
                {
                    string firstHalf = v.Substring(0, 3).ToUpper();
                    string secondHalf = v.Substring(3, 3).ToUpper();
                    bool alphabetic = Regex.IsMatch(firstHalf, @"^[a-zA-Z]+$");
                    bool numeric = int.TryParse(secondHalf, out _);
                    if (alphabetic != true || numeric != true)
                    {
                        ui.Print("The registration number most be in the form of \"ABC123\"!");
                    }
                    else
                    {
                        input = v.ToUpper();
                        done = true;
                    }
                }
            }
            return input;
        }

        private static IVehicle CreateSpecificVehicle()
        {
            IVehicle vehicle = null;
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
                ui.Print($"{type} was not a valid type of vehicle");
            }
            return vehicle;
        }

        private static IVehicle CreateAirplane(List<object> specifics, string type)
        {
            IVehicle vehicle;
            ui.Print($"How big is the wingspan of the {type} in meters?");
            double wingspan = LowerThenDoubleCheck(0);
            specifics.Add(wingspan);
            ui.Print($"How many engines does the {type} have?");
            int engines = LowerThenIntCheck(0);
            specifics.Add(engines);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static IVehicle CreateBoat(List<object> specifics, string type)
        {
            IVehicle vehicle;
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

        private static IVehicle CreateBus(List<object> specifics, string type)
        {
            IVehicle vehicle;
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

        private static IVehicle CreateCar(List<object> specifics, string type)
        {
            IVehicle vehicle;
            ui.Print($"What type of fuel is the {type} using?");
            string fuelType = ui.Input();
            specifics.Add(fuelType);
            ui.Print($"How manu seats are inside the {type}?");
            int numberOfSeats = LowerThenIntCheck(1);
            specifics.Add(numberOfSeats);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static IVehicle CreateMotorcycle(List<object> specifics, string type)
        {
            IVehicle vehicle;
            ui.Print($"What type of fuel is the {type} using?");
            string fuelType = ui.Input();
            specifics.Add(fuelType);
            vehicle = handler.CreateVehicle(specifics);
            return vehicle;
        }

        private static string BaseProp(List<object> specifics)
        {
            ui.Print("What type of vehicle do you want to add?");
            string type = Typecheck();
            specifics.Add(type);

            ui.Print($"What is the registration number for the {type}?");
            string registrationNumber = RegNumInput();
            specifics.Add(registrationNumber);

            ui.Print($"What color is the {type}?");
            string color = ui.Input();
            specifics.Add(color);

            ui.Print($"What is the number of wheels on the {type}?");
            int numberOfWheels = LowerThenIntCheck(0);
            specifics.Add(numberOfWheels);
            return type;
        }

        private static string Typecheck()
        {
            bool done = false;
            string output = new("");
            while (done != true)
            {
                var types = handler.TypesOfVehicles();
                output = ui.Input();
                foreach (var type in types)
                {
                    if (type.Name == output)
                    {
                        done = true;
                        break;
                    }
                }
                if (done != true)
                {
                    ui.Print("That was not an inplimented type." +
                        "\nPlease input a type that is in this list:");
                    foreach (var type1 in types)
                    {
                        ui.Print($"\t{type1}");
                    }
                }
            }
            return output;
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
                if (done != true || integer < lowestInt)
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
