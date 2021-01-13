using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.Models;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hackathon.Helpers
{
    public static class ExternalAPIs
    {
        public static Airports GetAirportInfo()
        {
            Airports airports = new Airports();
            airports.AirportList = new List<SelectListItem>();

            var client = new RestClient($"http://api.travelpayouts.com/data/en-GB/airports.json");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JArray objArray = (JArray)JsonConvert.DeserializeObject(response.Content);

            int counter = 0;
            var airportItems = new List<SelectListItem>();
            foreach (var item in objArray)
            {
                Airport airport = new Airport();
                airport.Name = item["name_translations"]["en"].ToString();
                airport.AirportCode = item["code"].ToString();
                airport.CountryCode = item["country_code"].ToString();
                airport.CityCode = item["city_code"].ToString();
                airportItems.Insert(counter, new SelectListItem { Text = airport.Name, Value = airport.AirportCode });
                counter++;
            }

            airports.AirportList = airportItems.OrderBy(a => a.Text);
            return airports;
        }

        public static Flights GetFlights(string originAirport, string destinationAirport, string depDate, string retDate)
        {
            Flights flights = new Flights();
            flights.OriginAirport = originAirport;
            flights.DestinationAirport = destinationAirport;
            flights.FlightList = new List<FlightData>();

            var client = new RestClient($"https://api.travelpayouts.com/v1/prices/cheap?origin={originAirport}&destination={destinationAirport}&depart_date={depDate}&return_date={retDate}&page=1&currency=usd");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Access-Token", "d618237d97fb3c802bef9d89bb1120e2");
            IRestResponse response = client.Execute(request);

            JObject responseObj = (JObject)JsonConvert.DeserializeObject(response.Content);

            if (responseObj["data"].HasValues)
            {
                JObject dataObj = responseObj["data"][destinationAirport].ToObject<JObject>();

                foreach (var obj in dataObj)
                {
                    JToken subObj = obj.Value;

                    FlightData flightData = new FlightData();
                    flightData.Price = subObj["price"].ToString();
                    flightData.Airline = subObj["airline"].ToString();
                    flightData.FlightNumber = subObj["flight_number"].ToString();
                    flightData.DepartureDate = subObj["departure_at"].ToString();
                    flightData.ReturnDate = subObj["return_at"].ToString();
                    flightData.ExpiryDate = subObj["expires_at"].ToString();
                    
                    flights.FlightList.Add(flightData);
                }
            }
            return flights;
        }

        public static List<WeatherData> GetWeatherInfo(string cityCode)
        {
            var client = new RestClient($"https://api.weatherbit.io/v2.0/forecast/daily?city={cityCode}&key=e4e2b3f9bd7846d98b29340589833c3f");
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);
            JObject responseObj = (JObject)JsonConvert.DeserializeObject(response.Content);
            List<WeatherData> weathers = new List<WeatherData>();
            if (responseObj["data"].HasValues)
            {
                foreach (var subObj in responseObj["data"].Take(5))
                {
                    WeatherData weatherData = new WeatherData();
                    weatherData.Description = subObj["weather"]["description"].ToString();
                    weatherData.Icon = subObj["weather"]["icon"].ToString().Substring(1,3)+".png";
                    weatherData.HighestTemp = subObj["max_temp"].ToString();
                    weatherData.LowesTemp = subObj["low_temp"].ToString();
                    weatherData.Temp = subObj["temp"].ToString();
                    weatherData.TempDate = DateTime.Parse(subObj["datetime"].ToString());
                    weathers.Add(weatherData);
                }
                
            }
            return weathers;
        }
    }
}
