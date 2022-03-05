using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Dtos.OpenWeather
{
    public class OpenWeatherRootDto
    {
        public maindto Main { get; set; }

        public winddto Wind { get; set; }

        public List<weatherdto> Weather { get; set; }
    }
}
