using Garage.Vehicles;
using System;

namespace Garage.Menu
{
    public class RemoveVehicleMenu : Menu
    {
        private GarageHandler GarageHandler;

        public RemoveVehicleMenu(String title, int width,GarageHandler garageHandler) : base(title, width)
        {
            GarageHandler = garageHandler;
            AddMenuRow(new MenuRow("Remove a vehicle", RemoveVehicle));
        }

        public static RemoveVehicleMenu CreateMenu(int width, GarageHandler garageHandler)
        {
            RemoveVehicleMenu menu = new RemoveVehicleMenu("VehicleMenu", width, garageHandler);
            return menu;
        }

        private void RemoveVehicle()
        {
            if (GarageHandler.GarageIsEmpty()) {
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
    }
}