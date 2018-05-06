using Garage.Vehicles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    public class GarageHandler
    {
        public delegate bool StringPredicate(string feature1, string feature2);
        public delegate bool IntPredicate(int feature1, int feature2);

        private Garage<Vehicle> Garage { get; }

        public GarageHandler(int capacity)
        {
            Garage = new Garage<Vehicle>(capacity);
        }

        private GarageHandler(Garage<Vehicle> garage)
        {
            Garage = garage;
        }

        public static GarageHandler LoadGarage(string name)
        {
            var loadedGarage = Garage<Vehicle>.Load(name);
            return new GarageHandler(loadedGarage);
        }

        public static String[] SavedFiles() {
            var path=Directory.GetCurrentDirectory();
            Console.WriteLine("path"+path);
            var files = Directory.GetFiles(path, "*.ser");
            foreach (string file in files) {
                Console.WriteLine(file);
            }
            return files;
        }

        public bool GarageIsFull()
        {
            return Garage.IsFull;
        }

        public bool GarageIsEmpty()
        {
            return Garage.VehicleCount == 0;
        }

        public static bool GarageExists(int capacity)
        {
            return Garage<Vehicle>.IsSaved(capacity);
        }


        public bool SaveGarage(string name)
        {
            return Garage.Save(name);
        }

        public bool Contains(string registrationNumber)
        {
            return Garage.Contains(registrationNumber);
        }

        public Vehicle GetVehicle(string registrationNumber)
        {
            return Garage.GetVehicle(registrationNumber);
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            if (Garage.IsFull || Garage.Contains(vehicle.RegistrationNumber))
            {
                return false;
            }
            else
            {
                Garage.Add(vehicle);
                return true;
            }
        }

        public bool RemoveVehicle(string registrationNumber)
        {
            if (!Garage.Contains(registrationNumber))
                return false;
            else
            {
                Garage.Remove(Garage.GetVehicle(registrationNumber));
                return true;
            }
        }

        public List<Vehicle> GetVehicles()
        {
            return Garage.ToList();
        }

        public String GarageInfo()
        {
            return Garage.ToString();
        }
                
        public IEnumerable<Tuple<string,int>> GroupByVehicleType()
        {
            var query = Garage.GroupBy(c => c.GetType())
                              .Select(c => new Tuple<string, int>(c.Key.Name, c.Count()));
            foreach (var item in query)
            {
                yield return item;
            }
        }

        public IEnumerable<Vehicle> GetVehiclesByColor(string color, StringPredicate pred)
        {
            return Garage.Where(c => pred(c.Color, color));
        }

        public IEnumerable<Vehicle> GetVehiclesByManufacturer(string manufact, StringPredicate pred)
        {
            return Garage.Where(c => pred(c.Manufacturer, manufact));
        }

        public IEnumerable<Vehicle> GetVehiclesByWheels(int wheels, IntPredicate pred)
        {
            return Garage.Where(c => pred(c.Wheels, wheels));
        }

        public IEnumerable<Vehicle> GetVehicles(string regNum, StringPredicate regPred, string color,
            StringPredicate colorPred, int wheels, string manufacturer, StringPredicate manufacturerPred)
        {
            IEnumerable<Vehicle> result = Garage;
            if (regPred != null)
            {
                result = result.Where(c => regPred(c.RegistrationNumber, regNum.ToUpper()));
            }
            if (colorPred != null) {
                result = result.Where(c => colorPred(c.Color, color.ToUpper()));
            }
            if (wheels > 0)
                result = result.Where(c => c.Wheels == wheels);
            if (manufacturerPred != null)
                result = result.Where(c => manufacturerPred(c.Manufacturer, manufacturer.ToUpper()));
            return result;
        }
    }
}
