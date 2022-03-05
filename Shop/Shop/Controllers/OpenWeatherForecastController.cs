using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos.OpenWeather;
using Shop.Core.ServiceInterface;
using Shop.Models.OpenWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class OpenWeatherForecastController : Controller
    {
        private readonly IOpenWeatherForecastServices _openWeatherForecastServices;

        public OpenWeatherForecastController
            (
                IOpenWeatherForecastServices openWeatherForecastServices
            )
        {
            _openWeatherForecastServices = openWeatherForecastServices;
        }

        [HttpGet]
        public IActionResult SearchOpenCity()
        {
            SearchOpenCity model = new SearchOpenCity();
            return View(model);
        }

        [HttpPost]
        public IActionResult SearchOpenCity(SearchOpenCity vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("OpenCity", "OpenWeatherForecast", new { openCity = vm.OpenCityName });
            }

            return View(vm);
        }
        [HttpGet]
        public IActionResult OpenCity(string openCity)
        {
            OpenWeatherResultDto dto = new OpenWeatherResultDto();
            dto.City = openCity;
            var weatherResponse = _openWeatherForecastServices.OpenWeatherDetail(dto);
            OpenCityViewModel vm = new OpenCityViewModel();

            vm.City = dto.City;
            vm.Temperature = dto.Temperature;
            vm.TempFeelsLike = dto.TempFeelsLike;
            vm.Humidity = dto.Humidity;
            vm.Pressure = dto.Pressure;
            vm.WindSpeed = dto.WindSpeed;
            vm.WeatherCondition = dto.WeatherCondition;

            return View(vm);
        }
    }
}
