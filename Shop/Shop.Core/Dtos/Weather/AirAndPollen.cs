using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Core.Dtos.Weather
{
    public class AirAndPollenDto
    {
        public string Name { get; set; }
        public Int32 Value { get; set; }
        public string Category { get; set; }
        public Int32 CategoryValue { get; set; }
        public string Type { get; set; }
    }
}
