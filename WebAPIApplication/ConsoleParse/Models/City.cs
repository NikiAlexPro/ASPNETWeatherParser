using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParse.Models
{
    public class City
    {
        
        public int Id { get; set; }
        
        public int RegionId { get; set; }

        public string CityName { get; set; }
        
        public string CityLink { get; set; }

    }
}
