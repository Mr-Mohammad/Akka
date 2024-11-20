using Akka.Actor;
using AkkaExample;
using Microsoft.AspNetCore.Mvc;

namespace AkkaExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IActorRef _requestProcessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IActorRef requestProcessor)
        {
            _logger = logger;
            _requestProcessor = requestProcessor;
        }
        [HttpPost("process")]
        public async Task<IActionResult> ProcessRequest([FromBody] string request)
        {
            var result = await _requestProcessor.Ask<string>(request, TimeSpan.FromSeconds(5));
            return Ok(new { message = result });
        }
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
