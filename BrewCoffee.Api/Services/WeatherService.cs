using System;
using System.Text.Json;

namespace BrewCoffee.Api.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<decimal?> GetCurrentTemperatureAsync()
    {
        try
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var city = _configuration["WeatherApi:City"] ?? "London";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Weather API returned {StatusCode}", response.StatusCode);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);
            var temperature = document.RootElement
                .GetProperty("main")
                .GetProperty("temp")
                .GetDecimal();

            return temperature;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get weather data");
            return null;
        }
    }
}
