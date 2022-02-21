using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Dtos.Weather
{
    public class WeatherRootDto
    {
        public HeadlineDto Headline { get; set; }
        public DailyForecastDto DailyForecast { get; set; }
    }
}
