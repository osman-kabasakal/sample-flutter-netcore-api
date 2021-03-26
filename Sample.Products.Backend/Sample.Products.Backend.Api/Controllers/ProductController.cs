using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.Core.Aspects.Concrete;

namespace Sample.Products.Backend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService,
            IServiceProvider serviceProvider
            )
        {
            _productService = TransparentProxy<IProductService>.GenerateProxy(productService,serviceProvider);
        }
        
        [HttpGet("get/{categoryId?}")]
        public IActionResult GetProducts(int? categoryId,[FromQuery]int pageIndex=0,[FromQuery]int pageSize=20)
        {
            return Ok(_productService.GetProductsByTagIds(categoryId==null?null:new int[]{categoryId.Value}, pageIndex: pageIndex, pageSize: pageSize));
        }
    }
}