using BlazorServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public partial class FetchData
    {
        public IWeatherForecastService _forecastService { get; } = new NewWeatherForecastService();

        private WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await _forecastService.GetForecastAsync(DateTime.Now);
        }
    }
}
