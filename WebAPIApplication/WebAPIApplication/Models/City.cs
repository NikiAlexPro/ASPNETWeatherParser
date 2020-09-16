using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApplication.Models
{
    public class City
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public string CityName { get; set; }

        public string CityLink { get; set; }
    }
}
