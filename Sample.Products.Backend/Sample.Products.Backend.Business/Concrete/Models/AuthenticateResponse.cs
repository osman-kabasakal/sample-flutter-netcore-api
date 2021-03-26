using System;
using Newtonsoft.Json;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Business.Concrete.Models
{
    [Serializable]
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

        public long Expire { get; set; }
        
        public AuthenticateResponse(Customer user, string jwtToken,long expire)
        {
            Id = user.Id;
            Email = user.Email;
            JwtToken = jwtToken;
            Expire = expire;
        }
    }
}