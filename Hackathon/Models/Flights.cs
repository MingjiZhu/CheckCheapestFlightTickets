using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Models
{
    public class Flights
    {
        public bool FlightsFound { get; set; } = false;
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public List<FlightData> FlightList { get; set; } = new List<FlightData>();
    }

    public class FlightData
    {
        public string Price { get; set; }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public string ExpiryDate { get; set; }
    }
}
