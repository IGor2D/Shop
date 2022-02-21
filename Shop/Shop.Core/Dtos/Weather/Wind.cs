using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Core.Dtos.Weather
{
    public class WindDto
    {
        public SpeedDto Speed { get; set; }
        public DirectionDto Direction { get; set; }
    }
}
