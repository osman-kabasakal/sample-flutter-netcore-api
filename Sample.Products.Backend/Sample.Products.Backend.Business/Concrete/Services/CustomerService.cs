using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Products.Backend.Business.Concrete.Attributes;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.Core.Constants;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Business.Concrete.Services
{
    [CatchErrorTypeForMethods]
    public class CustomerService:ICustomerService
    {
        private readonly IOptions<JwtOptions> _jwtOption;
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly RoleManager<RegisteredRole> _roleManager;

        public CustomerService(
            IOptions<JwtOptions> jwtOption,
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            RoleManager<RegisteredRole> roleManager
            )
        {
            _jwtOption = jwtOption;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public ServiceResponse<AuthenticateResponse> Authenticate(string username, string password)
        {
            var registeredUser =
                _userManager.FindByEmailAsync(username).ConfigureAwait(true).GetAwaiter().GetResult();
            if (registeredUser is null)
                throw new IdentityNotMappedException("Bilgileri kontrol ederek tekrar deneyiniz.");
            var signInResult = _signInManager.PasswordSignInAsync(registeredUser, password, false, false).ConfigureAwait(true)
                .GetAwaiter().GetResult();
            if (!signInResult.Succeeded)
                throw new IdentityNotMappedException("Şu an için hizmet veremiyoruz lütfen daha sonra tekrar deneyiniz.");
            var (token, expire) = GenerateJwtToken(registeredUser);
            return new ServiceResponse<AuthenticateResponse>()
            {
                IsSuccessful = true,
                Entity = new AuthenticateResponse(registeredUser,token,expire)
            };
        }
        
        
        
        public ServiceResponse<Customer> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<AuthenticateResponse> CreateFirstUser()
        {
            var email = "test@sample.com";
            var firstUser=_userManager.FindByEmailAsync(email).ConfigureAwait(true).GetAwaiter().GetResult();
            if(firstUser == null)
            {
                var use =_userManager.CreateAsync(new Customer(email, "Sample"), "Abc123+").GetAwaiter().GetResult();
                firstUser = _userManager.FindByEmailAsync(email).ConfigureAwait(true).GetAwaiter().GetResult();
                var roles = new[] { SampleRoles.Admin};
               
                roles.ToList().ForEach((val)=> {
                    _roleManager.CreateAsync(new RegisteredRole(val)).ConfigureAwait(false).GetAwaiter()
                        .GetResult();
                    _userManager.AddToRoleAsync(firstUser,val).ConfigureAwait(false).GetAwaiter().GetResult();
                });
            }

            return new ServiceResponse<AuthenticateResponse>()
            {
                IsSuccessful = true,
                Entity = new AuthenticateResponse(firstUser,"",DateTime.Now.Ticks)
            };
        }

        private (string,long) GenerateJwtToken(Customer user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOption.Value.Key);
            var expire = DateTime.UtcNow.AddMinutes(120);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                }),
                Issuer = _jwtOption.Value.Issuer,
                Audience = _jwtOption.Value.Audience,
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token),expire.Ticks);
        }
    }

    public interface ICustomerService
    {
        ServiceResponse<AuthenticateResponse> Authenticate(string username,string password);
        ServiceResponse<Customer> GetById(int id);

        ServiceResponse<AuthenticateResponse> CreateFirstUser();

    }
}