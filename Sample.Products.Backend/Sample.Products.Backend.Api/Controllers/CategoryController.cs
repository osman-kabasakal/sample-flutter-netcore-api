using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.Core.Aspects.Concrete;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ICategoryService categoryService,
            IServiceProvider serviceProvider
            )
        {
            _categoryService = TransparentProxy<ICategoryService>.GenerateProxy(categoryService,serviceProvider);
        }
        [HttpGet("getCategoryTree")]
        public IActionResult GetCategoryTree()
        {
            var categories = _categoryService.GetCategoryTree();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            return Ok(category);
        }
    }
}