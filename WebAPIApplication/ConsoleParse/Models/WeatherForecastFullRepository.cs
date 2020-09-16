using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParse.Models
{
    public class WeatherForecastFullRepository
    {
        WeatherForecastFullContext context = new WeatherForecastFullContext();
        
        public void Load(Region r)
        {
            var region = context.Region.Where(x => x.RegionName == r.RegionName).FirstOrDefault();
            if (region is null)
            {
                context.Region.Add(r);
            }
            else
            {
                region.RegionLink = r.RegionLink;
            }
        }

        public List<Region> GetRegions() //IEnumerable
        {
            //return context.Region.ToList();
            var regions = context.Region.ToList();
            return regions;
        }

        public List<City> GetCities()
        {
            //return context.City.ToList();
            //var cities = context.City.Where(x => x.CityName == "Брянск").ToList();
            var cities = context.City.ToList();
            return cities;
        }

        public void Load(City c)
        {

            var city = context.City.Where(x => x.CityName == c.CityName).FirstOrDefault();
            if (city is null)
            {
                context.City.Add(c);
            }
            else
            {
                city.CityLink = c.CityLink;
            }
        }

        public void Load(WeatherForecast w)
        {
            var weatherForecast = context.WeatherForecast.Where(x => x.CityId == w.CityId && x.WeatherDate == w.WeatherDate).FirstOrDefault();
            if (weatherForecast is null)
            {
                context.WeatherForecast.Add(w);
            }
            else
            {
                //Обновить все данные
                weatherForecast.TempDay = w.TempDay;
                weatherForecast.TempNight = w.TempNight;
                weatherForecast.Pressure = w.Pressure;
                weatherForecast.AirHumidity = w.AirHumidity;
                weatherForecast.WindDirection = w.WindDirection;
                weatherForecast.WeatherIconLink = w.WeatherIconLink;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
