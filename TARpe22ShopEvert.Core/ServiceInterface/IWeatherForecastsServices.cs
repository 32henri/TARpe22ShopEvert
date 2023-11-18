using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopEvert.Core.Dto.WeatherDtos;

namespace TARpe22ShopEvert.Core.ServiceInterface
{
    public interface IWeatherForecastsServices
    {
        Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto);
    }
}
