using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApplication.Models
{
    interface IRepository
    {
        WeatherForecast GetWeatherForecast(string cityName, int day, int month);
        //

    }
}
