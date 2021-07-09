using System.Linq;

namespace Exercise_5_Garage
{

    class Manager
    {
        static readonly IUI ui = new ConsoleUI();
        static readonly GarageHandler garageHandler = new();
        public static Garage<Vehicle> garage;
        public static void Start()
        {
            ui.Print("Welcome to the garage manager program");
            ui.Print("What would you like to do?");
            MenuText();
            bool exit = false;
            while (exit != true)
            {
                ui.Print("Give input." +
                    "\n7. Bring up menu text again.");
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
            bool exit = false;
            while (exit == false)
            {
                bool done1 = false;
                while (done1 == false)
                {
                    ui.Print("What is the max capacity of the garage?");
                    done1 = int.TryParse(ui.Input(), out int size);
                    if (done1 == true && size > 0)
                    {
                        bool done2 = false;
                        while (done2 == false)
                        {
                            ui.Print("How many vehicles are inside the garage from the start?");
                            done2 = int.TryParse(ui.Input(), out int filled);
                            if (done2 == true && filled >= 0)
                            {
                                garage = garageHandler.Initiate(size, filled);
                            }
                            else
                            {
                                ui.Print("Have to input an integer.");
                                done2 = false;
                            }

                        }
                    }
                    else
                    {
                        ui.Print("Have to input an integer bigger then 0.");
                        done1 = false;
                    }
                }
                break;
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
                                garage.Add(GarageHandler.CreateRandomVehicle(), true);
                                break;
                            case '2':
                                ui.Print("What type of vehicle do you want to add?");
                                Vehicle vehicle = GarageHandler.CreateVehicle();
                                garage.Add(vehicle, true);
                                break;
                            default:
                                ui.Print("Please enter some valid input (1, 2, 0)");
                                break;
                        }
                        break;
                    case '2':
                        ui.Print("Enter the registration number for the vehicle you want to remove.");
                        string input = ui.Input();
                        GarageHandler.RemoveVehicle(garage, input);
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
        private static void ListType()//Finds all subclasses of Vehicle and list the number of them in the garage
        {
            var assembly = typeof(Vehicle).Assembly;
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Vehicle)));
            foreach (var type in types)
            {
                ui.Print($"The number of {type.Name}: {GarageHandler.NumberOf(garage, type)}");
            }
        }
    }
}
