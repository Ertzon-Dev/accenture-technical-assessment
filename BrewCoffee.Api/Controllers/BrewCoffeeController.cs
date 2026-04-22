using BrewCoffee.Api.Models;
using BrewCoffee.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrewCoffee.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrewCoffeeController : ControllerBase
    {
        private readonly ICoffeeServices _coffeeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CallCountKey = "CoffeeBrewCallCount";

        public BrewCoffeeController(ICoffeeServices coffeeService, IHttpContextAccessor httpContextAccessor)
        {
            _coffeeService = coffeeService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("brew-coffee")]
        public async Task<IActionResult> GetBrewCoffee()
        {
            // Requirement #3: April 1st check
            if (_coffeeService.IsAprilFoolsDay())
            {
                return StatusCode(418); // I'm a teapot
            }

            // Get or initialize call count
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return StatusCode(500);

            var session = context.Session;
            var callCount = session.GetInt32(CallCountKey) ?? 0;
            callCount++;
            session.SetInt32(CallCountKey, callCount);

            // Requirement #2: Every 5th call returns 503
            if (callCount % 5 == 0)
            {
                return StatusCode(503);
            }

            // Requirement #1 + Extra Credit: Normal response with weather check
            var (message, prepared) = await _coffeeService.BrewCoffeeAsync();

            return Ok(new CoffeeResponse
            {
                Message = message,
                Prepared = prepared.ToString("yyyy-MM-ddTHH:mm:sszzz")
            });
        }
    }
}
