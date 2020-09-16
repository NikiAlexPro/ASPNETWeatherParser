using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WPF_Client
{
    class WeatherForecastDeserialize
    {
        [JsonProperty("IconWeather")]
        public string IconWeather { get; set; }

        [JsonProperty("CalendarDay")]
        public string CalendarDay { get; set; }

        [JsonProperty("CityName")]
        public string CityName { get; set; }

        [JsonProperty("WeatherDate")]
        public DateTime WeatherDate { get; set; }

        [JsonProperty("TempDay")]
        public string TempDay { get; set; }

        [JsonProperty("TempNight")]
        public string TempNight { get; set; }

        [JsonProperty("Pressure")]
        public string Pressure { get; set; }

        [JsonProperty("AirHumidity")]
        public string AirHumidity { get; set; }

        [JsonProperty("WindDirection")]
        public string WindDirection { get; set; }
    }
}
