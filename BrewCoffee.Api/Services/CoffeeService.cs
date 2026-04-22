using System.Runtime.CompilerServices;

namespace BrewCoffee.Api.Services;

public class CoffeeService : ICoffeeServices
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWeatherService _weatherService;
    private const string CallCountKey = "CoffeeBrewCallCount";

    public CoffeeService(
        IHttpContextAccessor httpContextAccessor,
        IWeatherService weatherService)
    {
        _httpContextAccessor = httpContextAccessor;
        _weatherService = weatherService;
    }

    public async Task<(string Message, DateTime Prepared)> BrewCoffeeAsync()
    {
        var prepared = DateTime.Now;
        var message = "Your piping hot coffee is ready";

        // Extra credit: Check weather for iced coffee
        var temperature = await _weatherService.GetCurrentTemperatureAsync();
        if (temperature.HasValue && temperature.Value > 30)
        {
            message = "Your refreshing iced coffee is ready";
        }

        return (message, prepared);
    }

    public bool IsAprilFoolsDay()
    {
        var today = DateTime.Today;
        return today.Month == 4 && today.Day == 1;
    }
}
