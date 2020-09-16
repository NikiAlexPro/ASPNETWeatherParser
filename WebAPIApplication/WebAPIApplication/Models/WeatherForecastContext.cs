using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;


namespace WebAPIApplication.Models
{
    public class WeatherForecastContext : DbContext
    {
        public DbSet<City> City { get; set; }

        public DbSet<WeatherForecast> WeatherForecast { get; set; }

        public WeatherForecastContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=weatherdb;user=root;password=Battlefield3");
            
        }
    }

}
