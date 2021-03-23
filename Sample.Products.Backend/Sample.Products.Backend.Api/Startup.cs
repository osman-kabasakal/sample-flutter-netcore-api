using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Products.Backend.DataAccess.Concrete.EntityFramework.Context;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Api
{
    public class Startup
    {
        private IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration,IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<SampleProductsContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString(_environment.IsDevelopment() ? "DebugConnection" : "DefaultConnection"), sqlServerOptionsAction: opt =>
                    {
                        opt.CommandTimeout(180);
                        opt.MigrationsAssembly("Sample.Products.Backend.Api");
                    });
                options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging();
            });
            
            // services.AddIdentity<Customer, RegisteredRole>()
            //     .AddDefaultTokenProviders()
            //     .AddEntityFrameworkStores<SampleProductsContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
