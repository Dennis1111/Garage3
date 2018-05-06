using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Menu
{
    public class GarageInitMenu : Menu
    {
        public GarageHandler GarageHandler { get; private set; }
        private String[] fileNames;

        public string[] FileNames { get => fileNames; set => fileNames = value; }

        public GarageInitMenu(String title, int width) : base(title, width)
        {
            FileNames = GarageHandler.SavedFiles();
            AddMenuRow(new MenuRow("Create a Garage", CreateGarage));
            AddMenuRow(new MenuRow("See saved Files", SavedFiles));
            AddMenuRow(new MenuRow("Load a saved garage ", LoadGarage));
            AddMenuRow(new MenuRow("LeaveMenu", LeaveInitMenu));
        }

        private void LeaveInitMenu()
        {
            if (GarageHandler == null)
            {
                Console.WriteLine("No Garage created yet");
                Console.WriteLine("Create one before leaving");

            }
            else
                OnLeaveMenu();
        }

        private void CreateGarage()
        {
            int garageSize = Ui.AskForInt("What capacity should the garage have");
            GarageHandler = new GarageHandler(garageSize);
        }


        private void SavedFiles()
        {
            Console.WriteLine("Saved Files");
            foreach (var file in FileNames)
                Console.WriteLine(file);
        }

        private void LoadGarage()
        {
            Console.WriteLine("What's the name of the garage to load (without path and .ser)\n");
            String name = Console.ReadLine();
            bool contains = false;
            foreach (string filename in fileNames)
            {
                if (filename.Contains(name))
                {
                    contains = true;
                    break;
                }
            }
            if (!contains)
            {
                Console.WriteLine($"No saved file with name {name}");
                return;
            }

            try
            {
                GarageHandler = GarageHandler.LoadGarage(name);
                if (GarageHandler == null)
                {
                    Console.WriteLine("Couldn't load that file");
                }
                else
                    Console.WriteLine("Garage loaded");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to load {name}");
            }
        }
    }
}
