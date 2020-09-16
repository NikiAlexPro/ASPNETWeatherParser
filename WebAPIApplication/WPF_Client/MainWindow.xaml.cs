using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace WPF_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         
        public MainWindow()
        {
            InitializeComponent();
            calendarWPF.SelectedDate = DateTime.Now;
            calendarWPF.DisplayDateStart = DateTime.Parse("1/1/" + DateTime.Now.Year);
            calendarWPF.DisplayDateEnd = DateTime.Parse("1/1/" + DateTime.Now.AddYears(1).Year);

            
            ObservableCollection<string> Strings = new ObservableCollection<string>();
            Strings.Add("Московский");
            Strings.Add("Москва");
            Strings.Add("Санкт-Петербург");
            ABC.ItemsSource = Strings;
        }

        WeatherForecastDeserialize weatherDeserialize = new WeatherForecastDeserialize();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            searchBT.IsEnabled = false;
            try
            {
                string cityName = textboxCityName.Text;
                string day = calendarWPF.SelectedDate.Value.Day.ToString();
                string month = calendarWPF.SelectedDate.Value.Month.ToString();
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create($"https://localhost:44372/weatherforecast/{cityName},{day},{month}");
                HttpWebResponse webResponse = (HttpWebResponse)await webRequest.GetResponseAsync();
                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (StreamReader streamread = new StreamReader(stream))
                    {
                        var weather = streamread.ReadToEnd();
                        weatherDeserialize = JsonConvert.DeserializeObject<WeatherForecastDeserialize>(weather);
                    }
                }
                webResponse.Close();

                svgTest.Source = new Uri(weatherDeserialize.IconWeather);
                calendarDay.Text = weatherDeserialize.CalendarDay;
                tempDay.Text = "Температура день: " + weatherDeserialize.TempDay;
                tempNight.Text = "Температура ночь: " + weatherDeserialize.TempNight;
                pressure.Text = "Давление: " + weatherDeserialize.Pressure;
                airHumidity.Text = "Влажность: " + weatherDeserialize.AirHumidity;
                windDirection.Text = "Ветер: " + weatherDeserialize.WindDirection;
                searchBT.IsEnabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                searchBT.IsEnabled = true;
            }
        }

        private void textboxCityName_GotFocus(object sender, RoutedEventArgs e)
        {
            textboxCityName.Clear();
        }
    }
}
