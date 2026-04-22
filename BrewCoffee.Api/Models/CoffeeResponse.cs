using System;

namespace BrewCoffee.Api.Models;

public class CoffeeResponse
{
    public string Message { get; set; } = string.Empty;
    public string Prepared { get; set; } = string.Empty;
}
