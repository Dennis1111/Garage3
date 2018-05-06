using Garage.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    [Serializable]
    public class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private T[] Vehicles;
        public int Capacity { get; }
        public int VehicleCount => this.Count();

        public Garage(int capacity)
        {
            Capacity = capacity;
            Vehicles = new T[capacity];
        }

        public override string ToString() => $"VehicleCount {VehicleCount} Empty spots {Capacity-VehicleCount}"; 

        public bool IsFull => VehicleCount == Capacity;

        public bool Add(T vehicle)
        {
            if (IsFull)
            {
                return false;
            }
            Vehicles[EmptyIndex()] = vehicle;            
            return true;
        }

        public bool Remove(T vehicle)
        {
            int indexOf = IndexOf(vehicle);
            if (indexOf < 0)
                return false;
            Vehicles[indexOf] = null;
            return true;
        }

        public bool Contains(String registrationNumber) => this.Any(c => c.RegistrationNumber == registrationNumber.ToUpper());

        public T GetVehicle(String registrationNumber)
        {
            return this.FirstOrDefault(c => c?.RegistrationNumber == registrationNumber.ToUpper());          
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var vehicle in Vehicles.Where(s => s!=null))
            {               
                yield return vehicle;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
                   GetEnumerator();

        private int IndexOf(T vehicle)
        {
            for (int i = 0; i < Capacity; i++)
                if (Vehicles[i] == vehicle)
                    return i;
            return -1;
        }

        private int EmptyIndex()
        {
            for (int i = 0; i < Capacity; i++)
                if (Vehicles[i] == null)
                    return i;
            return -1;
        }

        public bool Save(String filename) {
            FileStream fs = new FileStream(filename+".ser", FileMode.Create);
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, this);
                //Console.WriteLine("save the garage");
                return true;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                return false;
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
        
        public static Garage<T> Load(String filename)
        {
            Garage<T> loadedGarage = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(filename+".ser", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();                         
                loadedGarage = (Garage<T>) formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            return loadedGarage;
        }

        public static bool IsSaved(int capacity) {
            return File.Exists(GetFileName(capacity));
        }

        private static string GetFileName(int capacity)
        {
            return "garage" + capacity + ".ser";
        }
    }
}

