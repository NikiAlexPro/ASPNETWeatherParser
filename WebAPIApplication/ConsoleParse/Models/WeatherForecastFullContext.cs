using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleParse.Models
{
    public class WeatherForecastFullContext : DbContext
    {
        public DbSet<Region> Region { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<WeatherForecast> WeatherForecast { get; set; }
        

        public WeatherForecastFullContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=weatherdb;user=root;password=Battlefield3");

        }

        
    }
}
