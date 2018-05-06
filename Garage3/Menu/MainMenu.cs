using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage.Menu
{
    public class MainMenu : Menu
    {
        private GarageHandler GarageHandler;

        public MainMenu(String title,int width,GarageHandler GarageHandler): base(title,width)
        {
            this.GarageHandler = GarageHandler;
            AddMenuRow(new MenuRow("Add a vehicle", AddVehicleMenu.CreateMenu(width, GarageHandler)));
            AddMenuRow(new MenuRow("Remove a vehicle", RemoveVehicle));
            AddMenuRow(new MenuRow("Show all vehicles in garage", ShowVehicles));
            AddMenuRow(new MenuRow("Show vehicles by Group", GroupByVehicleType));
            AddMenuRow(new MenuRow("Search on vehicles", SerchOnVechicleFeatures));
            AddMenuRow(new MenuRow("Save garage", SaveGarage));
            AddMenuRow(new MenuRow("Leave the menu", OnLeaveMenu));
        }

        private void SaveGarage()
        {
            Console.Clear();
            Console.WriteLine("Enter a name for the garage to saved in");
            var filename = Console.ReadLine();
            if (GarageHandler.SaveGarage(filename))
                Console.WriteLine("The garage is now saved");
            else
                Console.WriteLine("Saving failed");
        }

        private void ShowVehicles()
        {
            Console.Clear();
            Console.WriteLine("Vehicles in the garage");
            if (!GarageHandler.GetVehicles().Any())
                Console.WriteLine("The Garage is empty");
            else
                foreach (var vehicle in GarageHandler.GetVehicles())
                    Console.WriteLine(vehicle);
            Console.WriteLine("Press any key to return to Main menu");
            Console.ReadKey();
            //OnLeaveMenu(this,this);
        }

        private void RemoveVehicle()
        {
            Console.Clear();
            if (GarageHandler.GarageIsEmpty())
            {
                Console.WriteLine("Garage is empty come back later");
                return;
            }
            do
            {
                string regNr = Ui.AskForString("Enter the registrationnumber for vehicle you want to remove",5,8);

                if (GarageHandler.Contains(regNr))
                {
                    GarageHandler.RemoveVehicle(regNr);
                }
                else
                {
                    Console.WriteLine($"Couldn't find a vehicle with registrationnumber {regNr} to remove");
                }
                Console.WriteLine("Press Y if want to remove more cars");
            } while (Console.ReadKey(intercept: true).Key == ConsoleKey.Y);
        }

        private void GroupByVehicleType()
        {
            Console.Clear();
            Console.WriteLine("Vehicle groups and how many vehicles in each group");
            foreach (var item in GarageHandler.GroupByVehicleType())
            {
                Console.WriteLine($"Vehicle group {item.Item1} contains {item.Item2} vehicles");
            }
            Console.WriteLine("Press any key to return to Main menu");
            Console.ReadKey();
        }

        private void SerchOnVechicleFeatures()
        {
            String regNum = "", color = "", manufacturer = "";
            int wheels = 0;
            GarageHandler.StringPredicate regNumPredicate = null, colorPredicate = null, manufacturerPredicate = null;
            Console.Clear();
            Console.WriteLine("Find a vehicle by features");
            Console.WriteLine("To search on registration number press Y");

            if (Console.ReadKey(intercept: true).Key == ConsoleKey.Y)
            {
                regNum = manufacturer = Ui.AskForString("Enter the registration number to search on",5,8);
                regNumPredicate = GetStringPredicate("Registration Number", regNum);
            }

            Console.WriteLine("To search on color press Y");
            if (Console.ReadKey(intercept: true).Key == ConsoleKey.Y)
            {
                Console.WriteLine("What Color do want to search on");
                color = Console.ReadLine();
                colorPredicate = GetStringPredicate("Color", color);
            }

            Console.WriteLine("To search on wheels press Y");
            if (Console.ReadKey(intercept: true).Key == ConsoleKey.Y)
            {
                wheels = Ui.AskForInt("How many wheels should the vehicle have");
            }
            Console.WriteLine("To search on manufacturer press Y");

            if (Console.ReadKey(intercept: true).Key == ConsoleKey.Y)
            {
                Console.WriteLine("What manufacturer do want to search on");
                manufacturer = Console.ReadLine();
                manufacturerPredicate = GetStringPredicate("Manufacturer", manufacturer);
            }

            var matchingVehicles = GarageHandler.GetVehicles(regNum, regNumPredicate, color, colorPredicate, wheels, manufacturer, manufacturerPredicate);
            Console.WriteLine($"Found {matchingVehicles.Count()} matching vehicles");
            foreach (var vehicle in matchingVehicles)
                Console.WriteLine(vehicle);
            Console.WriteLine("Press space to go back to main menu");
            do
            {
            } while (Console.ReadKey(intercept: true).KeyChar != ' ');
            this.Start();
        }

        private GarageHandler.StringPredicate GetStringPredicate(String feature, String name)
        {
            Console.WriteLine("Choose a search Criteria");
            Console.WriteLine($"1: To find vehicles with { feature} {name}");
            Console.WriteLine($"2: To find vechicle with {feature} that starts with {name}");
            Console.WriteLine($"3: To find any vehicle with {feature} containing {name}");

            char key;
      
            do
            {
                key = Console.ReadKey(intercept: true).KeyChar;
             
                switch (key)
                {
                    case '1':
                        return string.Equals;
                    case '2':
                        return StringStartsWith;
                    case '3':
                        return StringContains;
                    default:
                        Console.WriteLine("Wrong key try again");
                        break;
                }
            } while (true);         
        }

        private bool StringStartsWith(String feature, String subString)
        {
            return feature.StartsWith(subString);
        }

        private bool StringContains(String feature, String subString)
        {
            return feature.Contains(subString);
        }
    }
}
