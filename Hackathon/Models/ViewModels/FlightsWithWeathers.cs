using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Models.ViewModels
{
    public class FlightsWithWeathers
    {
        public Flights Flights { get; set; }
        public List<WeatherData> Weathers { get; set; }

    }
}
