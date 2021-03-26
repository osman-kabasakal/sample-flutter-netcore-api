using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.Core.Constants;
using Sample.Products.Backend.DataAccess.Concrete.EntityFramework.Context;
using Sample.Products.Backend.Entities.Concrete.Tables;
using SixLabors.ImageSharp;
using Microsoft.Extensions.Configuration;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace Sample.Products.Backend.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.ConfigureAppConfiguration(
                //(context, configure) =>
                //{
                //    if (context.HostingEnvironment.IsProduction())
                //    {
                //        var builtConfig = configure.Build();
                //        var secretClient = new SecretClient(
                //            new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                //            new DefaultAzureCredential());
                //        configure.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                //    }
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
