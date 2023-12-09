using Microsoft.AspNetCore.Mvc;
using TARpe22ShopEvert.Core.Dto.WeatherDtos;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Models.OpenWeather;
using TARpe22ShopEvert.Models.Weather;

namespace TARpe22ShopEvert.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IWeatherForecastsServices _openWeatherServices;
        public OpenWeatherController(IWeatherForecastsServices weatherForecastServices)
        {
            _openWeatherServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            OpenWeatherViewModel vm = new OpenWeatherViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult ShowWeather()
        {
            string city = Request.Form["City"];

            if (!string.IsNullOrEmpty(city))
            {
                OpenWeatherResultDto dto = new();
                dto.City = city;
                _openWeatherServices.OpenWeatherDetail(dto);

                OpenWeatherViewModel vm = new()
                {
                    City = dto.City,
                    Timezone = dto.Timezone,
                    Temperature = dto.Temperature,
                    Pressure = dto.Pressure,
                    Humidity = dto.Humidity,
                    Lon = dto.Lon,
                    Lat = dto.Lat,
                    Main = dto.Main,
                    Description = dto.Description,
                    Speed = dto.Speed
                };

                return View("City", vm);
            }

            return View();
        }
    }
}