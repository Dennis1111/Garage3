using Garage;
using Garage.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3
{
    class Program
    {
        static void Main(string[] args)
        {
            GarageHandler garageHandler = CreateGarageHandler();
            MainMenu mainMenu = new MainMenu("Main Menu", width: 200, GarageHandler: garageHandler);
            MenuHandler MenuHandler = new MenuHandler(mainMenu);
            MenuHandler.ActivateMenus();
        }

        private static GarageHandler CreateGarageHandler()
        {
            GarageInitMenu garageInitMenu = new GarageInitMenu("Garage Creation", 100);
            MenuHandler initMenuHandler = new MenuHandler(garageInitMenu);
            initMenuHandler.ActivateMenus();
            GarageHandler garageHandler = garageInitMenu.GarageHandler;
            return garageHandler;
        }
    }
}
