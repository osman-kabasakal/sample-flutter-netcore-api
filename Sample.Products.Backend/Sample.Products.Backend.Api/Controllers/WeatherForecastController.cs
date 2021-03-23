using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.Core.Aspects.Concrete;

namespace Sample.Products.Backend.Api.Controllers
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
        private readonly ITagService _tagService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITagService tagService,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _tagService = TransparentProxy<ITagService>.GenerateProxy(tagService, serviceProvider);
            // TransparentProxy<ITagService>.GenerateProxy(tagService,serviceProvider);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _tagService.GetTagById(1);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}