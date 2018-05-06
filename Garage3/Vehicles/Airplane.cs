using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Vehicles
{
    [Serializable]
    public class Airplane : Vehicle
    {
        public int PassengerSeats { get;}

        public Airplane(string registrationNumber, string color, int wheels, string manufacturer, int passengerSeats) : 
            base(registrationNumber,color,wheels,manufacturer)
        {
            PassengerSeats = passengerSeats;
        }

        public override string ToString()
        {
            return base.ToString() + ", Max Passengers " + PassengerSeats;
        }
    }
}
