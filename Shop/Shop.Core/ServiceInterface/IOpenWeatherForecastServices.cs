using Shop.Core.Dtos.OpenWeather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IOpenWeatherForecastServices : IApplicationService
    {
        Task<OpenWeatherResultDto> OpenWeatherDetail(OpenWeatherResultDto dto);
    }
}
