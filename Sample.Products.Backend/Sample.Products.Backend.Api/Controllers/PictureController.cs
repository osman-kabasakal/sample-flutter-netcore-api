using System;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.Core.Aspects.Concrete;

namespace Sample.Products.Backend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PictureController:ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(
            IPictureService pictureService,
            IServiceProvider serviceProvider
            )
        {
            _pictureService =TransparentProxy<IPictureService>.GenerateProxy(pictureService,serviceProvider);
        }

        [HttpGet("{id}")]
        public IActionResult GetPicture(int id)
        {
           var rt= _pictureService.GetPictureById(id);
           if (rt.IsSuccessful)
           {
               var uri = new Uri(rt.Entity.VirtualPath, UriKind.Relative);
               return base.Redirect(uri.ToString());
           }

           return BadRequest(rt);
        }
    }
}