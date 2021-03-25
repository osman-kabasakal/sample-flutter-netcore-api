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
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ITagService tagService,
            IServiceProvider serviceProvider,
            IProductService productService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _productService = TransparentProxy<IProductService>.GenerateProxy(productService, serviceProvider);
            _categoryService = TransparentProxy<ICategoryService>.GenerateProxy(categoryService, serviceProvider);
            _tagService = TransparentProxy<ITagService>.GenerateProxy(tagService, serviceProvider);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
           // var tag= _tagService.GetTagById(1);
           //  var cat=_categoryService.GetProductCategories(348);
           //  var product=_productService.GetProductById(1);
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