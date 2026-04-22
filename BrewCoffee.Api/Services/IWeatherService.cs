using System;

namespace BrewCoffee.Api.Services;

public interface IWeatherService
{
    Task<decimal?> GetCurrentTemperatureAsync();
}
