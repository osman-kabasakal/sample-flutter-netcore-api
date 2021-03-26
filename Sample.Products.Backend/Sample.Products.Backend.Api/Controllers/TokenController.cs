using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Products.Backend.Api.Models;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.Core.Aspects.Concrete;

namespace Sample.Products.Backend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class TokenController:ControllerBase
    {
        private readonly ICustomerService _customerService;

        public TokenController(
            ICustomerService customerService,
            IServiceProvider serviceProvider
            )
        {
            _customerService = TransparentProxy<ICustomerService>.GenerateProxy(customerService,serviceProvider);
        }

        [HttpPost("generate")]
        [AllowAnonymous]
        public IActionResult GenerateToken([FromBody] AuthenticateRequest model)
        {
            if (!ModelState.IsValid)
            {
                var rt = new ServiceResponse<bool>()
                {
                    IsSuccessful = false,
                    ExceptionMessage = string.Join(",",ModelState.Root.Errors.Select(x=>x.ErrorMessage).ToArray())
                };
                return BadRequest(rt);
            }

            var token = _customerService.Authenticate(model.Email, model.Password);
            
            if (token.IsSuccessful)
                return Ok(token);
            
            return BadRequest(token);
        }

        [HttpGet("create-user")]
        [AllowAnonymous]
        public IActionResult CreateUser()
        {
           return Ok(_customerService.CreateFirstUser());
        }
        
        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}