using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Vehicles
{
    [Serializable]
    public class Car : Vehicle
    {
        public bool Autonomous { get; }

        public Car(string registrationNumber, string color, int wheels, string manufacturer,Boolean autonomous) :
            base(registrationNumber,  color, wheels, manufacturer)
        {
            Autonomous = autonomous;                
        }

        public override string ToString() {
            return base.ToString() + ", Autonomous " + (Autonomous ? "Yes" : "No");
        }
    }
}
