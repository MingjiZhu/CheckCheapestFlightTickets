using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Models
{
    public class WeatherData
    {
        public string Description { get; set; }
        public string Icon { get; set; }
        public string HighestTemp { get; set; }
        public string LowesTemp { get; set; }
        public string Temp { get; set; }
        public DateTime TempDate { get; set; }
    }
}
