using System;

namespace BrewCoffee.Api.Services;

public interface ICoffeeServices
{
    Task<(string Message, DateTime Prepared)> BrewCoffeeAsync();
    bool IsAprilFoolsDay();
}
