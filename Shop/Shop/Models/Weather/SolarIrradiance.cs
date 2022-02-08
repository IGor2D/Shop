using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProceduresTask.Models.Weather
{
    public class SolarIrradiance
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public Int32 UnitType { get; set; }
    }
}
