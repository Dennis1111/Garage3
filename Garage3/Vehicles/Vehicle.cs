using System;

namespace Garage.Vehicles
{
    [Serializable]
    public abstract class Vehicle
    {
        public string RegistrationNumber { get;  }
        public string Color { get;  }
        public int Wheels { get;  }
        public string Manufacturer { get;  }        
     
        public Vehicle(string registrationNumber,string color,int wheels,string manufacturer)
        {
            RegistrationNumber = registrationNumber.ToUpper();
            Color = color.ToUpper();
            Wheels = wheels;
            Manufacturer = manufacturer.ToUpper();
        }
        
        public override string ToString() => "VehicleType "+GetType().Name+ ",  RegistrationNumber " + RegistrationNumber + ", Color " + Color + ", Wheels " + Wheels + ", Manufacturer " + Manufacturer;
    }
}