using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Vehicles
{
    [Serializable]
    class Bus : Vehicle
    {
        public string FuelType  { get;}

        public Bus(string registrationNumber, string color, int wheels, string manufacturer, string fuelType) :
           base(registrationNumber, color, wheels, manufacturer)
        {
            FuelType = fuelType;
        }

        public override string ToString()
        {
            return base.ToString() + ", FuelType " + FuelType;
        }

        public void save() {

        }
    }   
}
