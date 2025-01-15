using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;
using Serilog;
using Newtonsoft.Json;

namespace MCar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger _logger;

        public WeatherForecastController()
        {
            _logger = Log.Logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.Information("Getting weather forecast");
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]               
            })
            .ToArray();
            var dataJson = JsonConvert.SerializeObject(data);
            _logger.Information(dataJson);
            return data;
        }
    }
}
