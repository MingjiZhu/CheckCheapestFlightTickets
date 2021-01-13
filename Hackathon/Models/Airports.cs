using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Models
{
    public class Airports
    {
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        //public List<Airport> AirportList { get; set; } = new List<Airport>();
        public IEnumerable<SelectListItem> AirportList { get; set; }
    }
    public class Airport
    {
        public string Name { get; set; }
        public string AirportCode { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
    }
}
