using Nancy.Json;
using Shop.Core.Dtos.Weather;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
   public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto)
        {
            string apikey = "ReN3KQFP7EHUtwPlrs4BqfhaP3d5Z7Lo";
            var url = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/127964?apikey=%20%09ReN3KQFP7EHUtwPlrs4BqfhaP3d5Z7Lo%20&metric=true";
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                WeatherRootDto weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherRootDto>(json);
                
                //WeatherResultDto result = new WeatherResultDto();

                dto.EffectiveDate = weatherInfo.Headline.EffectiveDate;
                dto.EffectiveEpocDate = weatherInfo.Headline.EffectiveEpochDate;
                dto.Severity = weatherInfo.Headline.Severity;
                dto.Text = weatherInfo.Headline.Text;
                dto.Category = weatherInfo.Headline.Category;
                dto.EndDate = weatherInfo.Headline.EndDate;
                dto.EndEpochDate = weatherInfo.Headline.EndEpochDate;
                dto.MobileLink = weatherInfo.Headline.MobileLink;
                dto.Link = weatherInfo.Headline.Link;
                //result.DailyForecastsDate = weatherInfo.DailyForecast.Date;
                //result.DailyForecastsEpochDate = weatherInfo.DailyForecast.EpochDate;
                //result.TempMinValue = weatherInfo.DailyForecast.Temperature.Minimum.Value;
                //result.TempMinUnit = weatherInfo.DailyForecast.Temperature.Minimum.Unit;
                //result.TempMinUnitType = weatherInfo.DailyForecast.Temperature.Minimum.UnitType;

                var jsonString = new JavaScriptSerializer().Serialize(dto);
                //return jsonString;
            }
            return dto;
        }
    }
}
