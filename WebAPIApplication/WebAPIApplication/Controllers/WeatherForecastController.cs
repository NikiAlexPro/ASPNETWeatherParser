using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using System;
using System.Linq;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IRepository repository;

        public WeatherForecastController()
        {
            IKernel ninjectkernel = new StandardKernel();
            ninjectkernel.Bind<IRepository>().To<WeatherForecastRepository>();
            repository = ninjectkernel.Get<IRepository>();
        }


        [HttpGet("{city},{day},{month}")]
        public IActionResult Get(string city, int day, int month)
        {

            WeatherForecast weatherForecast = repository.GetWeatherForecast(city, day, month);


            return Ok(new
            {
                IconWeather = weatherForecast.WeatherIconLink,
                CalendarDay = weatherForecast.WeatherDate,
                CityName = city,
                WeatherDate = weatherForecast.WeatherDate,
                TempDay = weatherForecast.TempDay,
                TempNight = weatherForecast.TempNight,
                Pressure = weatherForecast.Pressure,
                AirHumidity = weatherForecast.AirHumidity,
                WindDirection = weatherForecast.WindDirection
            });
        }
    }
}
