using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hackathon.Models;
using Hackathon.Helpers;
using Hackathon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Airports airports = ExternalAPIs.GetAirportInfo();

            return View(airports);
        }

        [HttpGet("Search")]
        public IActionResult Search(string depDate, string retDate, string originAirport, string destinationAirport)
        {
            try
            {
                FlightsWithWeathers model = new FlightsWithWeathers();
                model.Flights = ExternalAPIs.GetFlights(originAirport, destinationAirport, depDate, retDate);
                model.Weathers = ExternalAPIs.GetWeatherInfo(destinationAirport);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
