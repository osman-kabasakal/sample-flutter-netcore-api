using System;
using Microsoft.AspNetCore.Identity;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public sealed class Customer:IdentityUser,IEntity
    {
        public Customer()
        {
            
        }
        public Customer(string email):base(email)
        {
            this.Email = email;
        }

        public Customer(string email, string UserName):base(UserName)
        {
            this.Email = email;
        }
    }
}