using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ConsoleParse.Models;

namespace ConsoleParse
{
    class ConsoleParseWeatherForecast
    {
        //Парсинг страниц и запись в БД
        static void Main()
        {
            //IRepository repository;
            //IKernel ninjectkernel = new StandardKernel();
            //ninjectkernel.Bind<IRepository>().To<WeatherForecastFullRepository>();
            //repository = ninjectkernel.Get<IRepository>();

            WeatherForecastFullRepository repository = new WeatherForecastFullRepository();

            Dictionary<string, int> dayMonthPairs = new Dictionary<string, int>()
            {
                {"января" , 1 },
                {"февраля" , 2 },
                {"марта" , 3 },
                {"апреля" , 4 },
                {"мая" , 5 },
                {"июня" , 6 },
                {"июля" , 7 },
                {"августа" , 8 },
                {"сентября" , 9 },
                {"октября" , 10 },
                {"ноября" , 11 },
                {"декабря" , 12 }
            };

            var workerTimer = new Timer(x => {
               try
               {
                    ////Регионы
                    //GetRegions();
                    //repository.Save();

                    ////Города
                    //GetСities();
                    //repository.Save();
                    
                    //Погода
                    GetWeatherForecast();
                    repository.Save();
                }
                catch (Exception ex)
               {
                    Console.WriteLine(ex);
                }
           }, null, dueTime: TimeSpan.Zero, period: TimeSpan.FromMinutes(5));
            Console.ReadLine();
            
            void GetRegions()
            {
                var html = @"https://yandex.ru/pogoda/region/225?via=reg";
                var web = new HtmlWeb();
                var htmlDoc = web.Load(html);
                var testNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='place-list']//a[@href]");

                foreach(var node in testNodes)
                {
                    repository.Load(new Region() { RegionName = node.InnerText, RegionLink = node.Attributes["href"].Value });
                }
            }

            void GetСities()
            {
                var regionsCollection = repository.GetRegions();
                foreach(var region in regionsCollection)
                {
                    var html = @"https://yandex.ru" + region.RegionLink;
                    var web = new HtmlWeb();
                    var htmlDoc = web.Load(html);
                    var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='place-list']//a[@href]");
                    foreach(var node in nodes)
                    {
                        repository.Load(new City() { RegionId = region.Id, CityName = node.InnerText, CityLink = node.Attributes["href"].Value });
                    }
                    Console.WriteLine(region.Id);
                }
            }

            void GetWeatherForecast()
            {
                Console.WriteLine("Hello");
                var citiesCollection = repository.GetCities();
                try
                {
                    foreach (var city in citiesCollection)
                    {
                        var html = @"https://yandex.ru" + city.CityLink;
                        var web = new HtmlWeb();
                        var htmlDoc = web.Load(html);
                        var node = htmlDoc.DocumentNode.SelectSingleNode("(//div[@class='forecast-briefly__header']//a)[2]");
                        var newhtml = @"https://yandex.ru" + node.Attributes["href"].Value;
                        var newhtmlDoc = web.Load(newhtml);
                        var nodes = newhtmlDoc.DocumentNode.SelectNodes("//div[@class='climate-calendar-container__calendar']//span/a");//Загрузка на каждый месяц
                        foreach (var nod in nodes.Skip(1).Take(12))
                        {
                            var htmlMonth = @"https://yandex.ru" + nod.Attributes["href"].Value;
                            var htmlMonthDoc = web.Load(htmlMonth);
                            var nodeMonths = htmlMonthDoc.DocumentNode.SelectNodes("//div[@class='climate-calendar-day__day']/../..//div[@class='climate-calendar-day__detailed-container-center']");
                            foreach (var nodeDay in nodeMonths)
                            {
                                //2-температура днем 1-температура ночью
                                var tempWeatherArray = nodeDay.SelectNodes("*//div").Where(x => x.Name == "div").ToArray();

                                //1-давление 3-влажность 5 - скорость ветра
                                var paramWeatherArray = nodeDay.SelectNodes("*//td").Where(x => x.Name == "td").ToArray();

                                //WeatherIconLink
                                var weatherIcon = @"https:" + nodeDay.SelectSingleNode("//img").Attributes["src"].Value;

                                //Дата
                                var dayMonth = nodeDay.SelectSingleNode("h6").InnerText;//   Проверить h6 или *h6
                                string[] splitDayMonth = dayMonth.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                                DateTime weatherdate = DateTime.Parse($"{splitDayMonth[0]}/{dayMonthPairs[splitDayMonth[1]]}/{DateTime.Now.Year.ToString()}").Date;

                                repository.Load(new WeatherForecast()
                                {
                                    CityId = city.Id,
                                    WeatherDate = weatherdate,
                                    TempDay = tempWeatherArray[2].InnerText,
                                    TempNight = tempWeatherArray[1].InnerText,
                                    Pressure = paramWeatherArray[1].InnerText,
                                    AirHumidity = paramWeatherArray[3].InnerText,
                                    WindDirection = paramWeatherArray[5].InnerText,
                                    WeatherIconLink = weatherIcon
                                });
                                //Добавление в БД  

                            }
                            Console.WriteLine($"    Месяц: {nod.InnerText} города: {city.CityName}");
                        }
                        Console.WriteLine($"Город: {city.CityName} --- City-Id = {city.Id} / RegionId = {city.RegionId} --- {citiesCollection.Count()}");
                        //Сохранение ТЕСТ
                        repository.Save();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }



        }
        
        
    }
}
