using Garage.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage.Menu
{
    class AddVehicleMenu : Menu
    {
        private GarageHandler GarageHandler;

        public AddVehicleMenu(String title, int width, GarageHandler garageHandler) : base(title, width)
        {
            GarageHandler = garageHandler;
            AddMenuRow(new MenuRow("Add a car", AddCar));
            AddMenuRow(new MenuRow("Add a motorcycle", AddMotorCycle));
            AddMenuRow(new MenuRow("Add a boat", AddBoat));
            AddMenuRow(new MenuRow("Add an airPlane", AddAirPlane));
            AddMenuRow(new MenuRow("Add a bus", AddBus));
            AddMenuRow(new MenuRow("Leave the menu", OnLeaveMenu));
        }

        public static AddVehicleMenu CreateMenu(int width, GarageHandler garageHandler)
        {
            AddVehicleMenu menu = new AddVehicleMenu("Add a vehicle menu", width, garageHandler);
            return menu;
        }

        private void AddCar()
        {
            Console.Clear();
            if (GarageHandler.GarageIsFull())
            {
                Console.WriteLine("Garage is full come back later");
                return;
            }
            do
            {
                Console.WriteLine("Add a car");
                string registrationNumber = Ui.AskForRegistrationNumber(GarageHandler);
                string color = Ui.AskForString("Enter a Color", minLength:3 ,maxLength:20);
                int wheels = Ui.AskForVehicleInt("Car", "Wheels", min: 3, max: 4);
                string manufacturer = Ui.AskForString("Enter a manufactuer",minLength: 3, maxLength: 20);
                bool autonomous = Ui.AskForBool("Is the car autonomous");
                Vehicle vehicle = new Car(registrationNumber, color, wheels, manufacturer, autonomous);
                bool added = GarageHandler.AddVehicle(vehicle);
                Console.WriteLine($"You added the Vehicle\n {vehicle}\nto the Garage");
                if (GarageHandler.GarageIsFull())
                {
                    Console.WriteLine("Garage is now full");
                    break;
                }
                Console.WriteLine("Press Y if want to Add more cars");
            } while (Console.ReadKey(intercept: true).Key == ConsoleKey.Y);
        }

        private void AddBoat()
        {
            Console.Clear();
            if (GarageHandler.GarageIsFull())
            {
                Console.WriteLine("Garage is full come back later");
                return;
            }
            do
            {
                Console.WriteLine("Add a boat");
                string registrationNumber = Ui.AskForRegistrationNumber(GarageHandler);

                string color = Ui.AskForString("Enter a Color", minLength: 3, maxLength: 20);
                string manufacturer = Ui.AskForString("Enter a manufactuer", minLength: 3, maxLength: 20);

                bool cargoShip = Ui.AskForBool("Is it a cargo ship");
                Vehicle vehicle = new Boat(registrationNumber, color, manufacturer, cargoShip);
                GarageHandler.AddVehicle(vehicle);
                Console.WriteLine($"You added the Vehicle {vehicle} to the Garage");
                if (GarageHandler.GarageIsFull())
                {
                    break;
                }
                Console.WriteLine("Press Y if want to Add more boats");
            } while (Console.ReadKey(intercept: true).Key == ConsoleKey.Y);
        }

        private void AddAirPlane()
        {
            Console.Clear();
            if (GarageHandler.GarageIsFull())
            {
                Console.WriteLine("Garage is full come back later");
                return;
            }
            do
            {
                Console.WriteLine("Add an airplane");
                string registrationNumber = Ui.AskForRegistrationNumber(GarageHandler);
                string color = Ui.AskForString("Enter a Color", minLength: 3, maxLength: 20);
                int wheels = Ui.AskForVehicleInt("Airplane", "wheels", min: 4, max: 20);
                string manufacturer = Ui.AskForString("Enter a manufactuer", minLength: 3, maxLength: 20);
                int passengerSeats = Ui.AskForVehicleInt("Airplane", "passengerseats", min: 1, max: 6000);
                Vehicle vehicle = new Airplane(registrationNumber, color, wheels, manufacturer, passengerSeats);
                GarageHandler.AddVehicle(vehicle);
                Console.WriteLine($"You added the Vehicle {vehicle} to the Garage");
                if (GarageHandler.GarageIsFull())
                {
                    break;
                }
                Console.WriteLine("Press Y if want to Add more airPlanes");
            } while (Console.ReadKey(intercept: true).Key == ConsoleKey.Y);
        }

        private void AddBus()
        {
            Console.Clear();
            if (GarageHandler.GarageIsFull())
            {
                Console.WriteLine("Garage is full come back later");
                return;
            }

            do
            {

                Console.WriteLine("Add a bus");
                string registrationNumber = Ui.AskForRegistrationNumber(GarageHandler);
                string color = Ui.AskForString("Enter a Color", minLength: 3, maxLength: 20);
                int wheels = Ui.AskForVehicleInt("Bus", "wheels", min: 4, max: 100);
                string manufacturer = Ui.AskForString("Enter a manufactuer", minLength: 3, maxLength: 20);
                Console.WriteLine("Whats's the bus fueltype");
                string fuelType = Console.ReadLine();
                Vehicle vehicle = new Bus(registrationNumber, color, wheels, manufacturer, fuelType);
                GarageHandler.AddVehicle(vehicle);
                Console.WriteLine($"You added the Vehicle {vehicle} to the Garage");
                if (GarageHandler.GarageIsFull())
                {
                    break;
                }
                Console.WriteLine("Press Y if want to Add more another bus");
            } while (Console.ReadKey(intercept: true).Key == ConsoleKey.Y);
        }

        private void AddMotorCycle()
        {

            Console.Clear();
            if (GarageHandler.GarageIsFull())
            {
                Console.WriteLine("Garage is full come back later");
                return;
            }

            do
            {
                Console.WriteLine("Add a motorcycle");
                string registrationNumber = Ui.AskForRegistrationNumber(GarageHandler);
                string color = Ui.AskForString("Enter a Color", minLength: 3, maxLength: 20);
                int wheels = Ui.AskForVehicleInt("motorcycle", "wheels", min: 2, max: 3);
                string manufacturer = Ui.AskForString("Enter a manufactuer", minLength: 3, maxLength: 20);
                bool sidecar = Ui.AskForBool("Has the motorcycle a sidecar");
                Vehicle vehicle = new MotorCycle(registrationNumber, color, wheels, manufacturer, sidecar);
                GarageHandler.AddVehicle(vehicle);
                Console.WriteLine($"You added the Vehicle {vehicle} to the Garage");
                if (GarageHandler.GarageIsFull())
                {
                    break;
                }
                Console.WriteLine("Press Y if want to Add more motorCycles");
            } while (Console.ReadKey(intercept: true).Key == ConsoleKey.Y);
        }
    }
}
