using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Core.Dtos.Weather
{
    public class DailyForecastDto
    {
        public string Date { get; set; }
        public Int64 EpochDate { get; set; }
        public SunDto Sun { get; set; }
        public MoonDto Moon { get; set; }
        public TemperatureDto Temperature { get; set; }
        public RealFeelTemperatureDto RealFeelTemperature { get; set; }
        public RealFeelTemperatureShadeDto RealFeelTemperatureShade { get; set; }
        public DegreeDaySummaryDto DegreeDaySummary { get; set; }
        public AirAndPollenDto AirAndPollen { get; set; }
        public DayDto DayDto { get; set; }
        public NightDto Night { get; set; }
        public string Sources { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }
}
