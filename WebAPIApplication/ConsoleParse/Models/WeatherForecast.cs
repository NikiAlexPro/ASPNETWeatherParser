using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParse.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public DateTime WeatherDate { get; set; }

        public string TempDay { get; set; }

        public string TempNight { get; set; }

        public string Pressure { get; set; }

        public string AirHumidity { get; set; }

        public string WindDirection { get; set; }

        public string WeatherIconLink { get; set; }
    }
}
