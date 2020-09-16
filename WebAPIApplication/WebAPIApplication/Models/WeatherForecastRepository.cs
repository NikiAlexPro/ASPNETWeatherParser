using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApplication.Models
{
    public class WeatherForecastRepository : IDisposable, IRepository
    {

        WeatherForecastContext context = new WeatherForecastContext();

        //Можно Добавить выбор региона
        public WeatherForecast GetWeatherForecast(string cityName, int day, int month)
        {
            DateTime targetDateTime = DateTime.Parse($"{day}/{month}/{DateTime.Now.Year.ToString()}");
            var targetCity = context.City.Where(x => x.CityName == cityName).FirstOrDefault();
            var targetWeatherForecastOfCity = context.WeatherForecast.Where(x => x.CityId == targetCity.Id && x.WeatherDate == targetDateTime).FirstOrDefault();
            return targetWeatherForecastOfCity;
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
