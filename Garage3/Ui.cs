using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal class Ui
    {
        public static string AskForString(string message,int minLength,int maxLength)
        {
            string input = "";
            bool badInput;
            do
            {
                badInput = false;
                Console.WriteLine(message);
                input = Console.ReadLine();
                if (input.Length < minLength)
                {
                    Console.WriteLine($"To short name ,min is {minLength}");
                    badInput = true;
                }
                if (input.Length > maxLength)
                {
                    Console.WriteLine($"To long name ,max is {maxLength}");
                    badInput = true;
                }

            } while (badInput);
            return input;
        }

        //Should probably not use garagehandler in UI be and also 
        //maybe fetch minLength,maxlength from Vehicle class
        public static string AskForRegistrationNumber(GarageHandler garageHandler)
        {
            bool regNrExists;
            string regNo = "";
            do
            {
                regNrExists = false;
                regNo = AskForString("Enter a registrationnumber", minLength: 5, maxLength: 8);
                if (garageHandler.Contains(regNo))
                {
                    regNrExists = true;
                    Console.WriteLine($"RegistrationNumber {regNo} already exists in Garage");
                }
            } while (regNrExists);
            return regNo;
        }

        public static int AskForVehicleInt(string vehicleType, string feature, int min, int max)
        {            
            int wheels = 0;
            do
            {
                wheels = AskForInt($"How many {feature} do the {vehicleType} have");
                if (wheels < min)
                    Console.WriteLine($"{feature} must be atleast {min}");
                if (wheels > max)
                    Console.WriteLine($"{feature} must be at most {max}");
            } while (wheels < min || wheels > max);
            return wheels;
        }

        public static int AskForInt(string question,int min)
        {
            bool okRange;
            int value;
            do
            {
               
                value = AskForInt(question);
                okRange = value < min; 
                if (!okRange)
                {
                    Console.WriteLine($"The number should be>={min}");
                }

            } while (!okRange);
            return value;
        }

        public static int AskForInt(string question)
        {
            bool parsed;
            int value;

            do
            {
                string answer = Ask(question);
                parsed = int.TryParse(answer, out value);               
                if (!parsed)
                {
                    Console.WriteLine("Only numbers is allowed");
                }

            } while (!parsed);
            return value;
        }

        public static bool AskForBool(string question)
        {
            Console.WriteLine(question);
            Console.WriteLine("Press y for yes");
            var key = Console.ReadKey(intercept :true).Key;
            return key == ConsoleKey.Y ? true : false;
        }


        public static string Ask(string question)
        {
            Console.Write(question + " ");
            return Console.ReadLine();
        }
    }
}
