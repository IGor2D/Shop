using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Core.Dtos.Weather
{
    public class TemperatureDto
    {
        public MinimumDto Minimum { get; set; }
        public MaximumDto Maximum { get; set; }
    }
}
