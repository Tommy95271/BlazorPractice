using BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class FetchDataBase : ComponentBase
    {
        [Inject]
        public IWeatherForecastService _forecastService { get; set; }

        public WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await _forecastService.GetForecastAsync(DateTime.Now);
        }
    }
}
