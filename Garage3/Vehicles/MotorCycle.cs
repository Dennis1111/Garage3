using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Vehicles
{
    [Serializable]
    class MotorCycle : Vehicle
    {
        public bool SideCar { get; }

        public MotorCycle(string registrationNumber, string color, int wheels, string manufacturer,bool sideCar) : base(registrationNumber, color, wheels, manufacturer)
        {
            SideCar = sideCar;
        }

        public override string ToString()
        {
            return base.ToString() + ", SideCar " + (SideCar ? "Yes" : "No");
        }
    }
}
