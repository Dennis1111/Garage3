using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Vehicles
{
    [Serializable]
    class Boat : Vehicle
    {
        public bool CargoShip { get; }

        public Boat(string registrationNumber, string color, string manufacturer, bool cargoShip) :
            base(registrationNumber, color, 0, manufacturer)
        {
            CargoShip = cargoShip;
        }

        public override string ToString()
        {
            return base.ToString() + ", CargoShip " + (CargoShip ? "Yes" : "No");
        }
    }
}
