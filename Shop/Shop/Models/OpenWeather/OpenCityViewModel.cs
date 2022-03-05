using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.OpenWeather
{
    public class OpenCityViewModel
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public double TempFeelsLike { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string WeatherCondition { get; set; }
    }
}
