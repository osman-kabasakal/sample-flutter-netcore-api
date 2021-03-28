using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.Core.Aspects.Concrete;
using Sample.Products.Backend.Core.Aspects.Interfaces;
using Sample.Products.Backend.Core.Constants;
using Sample.Products.Backend.DataAccess.Concrete.EntityFramework.Context;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork.DependencyInjection;
using Sample.Products.Backend.Entities.Abstract;
using Sample.Products.Backend.Entities.Concrete.Tables;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Sample.Products.Backend.Api
{
    public class Startup
    {
        private IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment
        )
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.Culture = new CultureInfo("tr-TR");
                x.UseCamelCasing(true);
                // x.UseMemberCasing();
                x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddHttpContextAccessor();
            SeedData.Seder = new SetupManager(new FileService(_environment));
            services.AddAutoMapper(
                Assembly.GetAssembly(typeof(SampleProductsContext)),
                Assembly.GetAssembly(typeof(TagService)),
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(AspectContext)));

            services.AddUnitOfWork<SampleProductsContext>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddHttpClient<IPictureService, PictureService>();
            services.AddScoped<IFileService, FileService>();
            services.AddHttpClient<IUnsplashClient, UnsplashClient>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISetupManager, SetupManager>();
            services.AddDbContext<SampleProductsContext>(options =>
            {
                var conString = _environment.IsDevelopment()
                    ? Configuration
                        .GetConnectionString("DebugConnection").TrimEnd(';', ' ') + ";MultipleActiveResultSets=true;"
                    : Configuration
                        .GetConnectionString("DefaultConnection");
                // conString = Configuration
                //     .GetConnectionString("DefaultConnection");
                options.UseSqlServer(
                    conString
                    , sqlServerOptionsAction: opt =>
                    {
                        opt.CommandTimeout(180);
                        opt.MigrationsAssembly("Sample.Products.Backend.Api");
                    });
                options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging();
            });
            // services.AddScoped<UserManager<Customer>>();

            services
                .AddIdentity<Customer, RegisteredRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<SampleProductsContext>();

            var appSettingsSection = Configuration.GetSection("JwtOptions");
            services.Configure<JwtOptions>(appSettingsSection);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = Configuration["JwtOptions:Audience"];
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.ClaimsIssuer = Configuration["JwtOptions:Issuer"];
                var key = Configuration["JwtOptions:Key"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = true
                };
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = (context) =>
                    {
                        //context.Principal.Identity is ClaimsIdentity
                        //So casting it to ClaimsIdentity provides all generated claims
                        //And for an extra token validation they might be usefull
                        var name = context.Principal.Identity.Name;
                        if (string.IsNullOrEmpty(name))
                        {
                            context.Fail("Unauthorized. Please re-login");
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = (context) => { return Task.CompletedTask; }
                };
            });
            // services.AddScoped<ITagService>(x=>);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }


            app.UseRouting();
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            // app.UseStaticFiles(new StaticFileOptions()
            // {
            //     FileProvider = app.ApplicationServices.GetRequiredService<IFileService>(),
            //     RequestPath = new PathString("/images")
            // });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=WeatherForecast}/{action=Get}");
            });
            if (true)
            {
                // return;
            }

            var host = app.ApplicationServices;
            using (var scope = host.CreateScope())
            {
                // requires using Microsoft.Extensions.Configuration;
                // var userManager = host.GetService(typeof(UserManager<Customer>)) as UserManager<Customer>;
                // var email = "test@sample.com";
                // Set password with the Secret Manager tool.
                // dotnet user-secrets set SeedUserPW <pw>
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SampleProductsContext>();
                    context.Database.Migrate();
                    //var productService = services.GetRequiredService<IProductService>();
                    //var product = productService.GetAllProducts(0, 1);
                    //if (product.IsSuccessful&&product.Entity.Count==0)
                    //{
                    //    var setupManager = services.GetRequiredService<ISetupManager>();
                    //    setupManager.setup();
                    //    var pictureService = services.GetRequiredService<IPictureService>();

                    //    var pics = pictureService.GetBinaryLessPictures();

                    //    foreach (var picture in pics.Entity.Items)
                    //    {
                    //        pictureService.SetPictureBinaryFromFile(picture);
                    //    }
                    //}

                    // var FirstUser=userManager.FindByEmailAsync(email).ConfigureAwait(true).GetAwaiter().GetResult();
                    // if(FirstUser == null)
                    // {
                    //     var use =userManager.CreateAsync(new Customer(email, "Sample"), "Abc123+").GetAwaiter().GetResult();
                    //     FirstUser = userManager.FindByEmailAsync(email).ConfigureAwait(true).GetAwaiter().GetResult();
                    //     var roles = new[] { SampleRoles.Admin};
                    //     var roleManager = host.GetRequiredService<RoleManager<RegisteredRole>>();
                    //     roles.ToList().ForEach((val)=> {
                    //         roleManager.CreateAsync(new RegisteredRole(val)).ConfigureAwait(false).GetAwaiter()
                    //             .GetResult();
                    //         userManager.AddToRoleAsync(FirstUser,val).ConfigureAwait(false).GetAwaiter().GetResult();
                    //     });
                    // }

                    // foreach (var picture in pictureService.AllPicture().Entity.Items)
                    // {
                    //     picture.TitleAttribute = $"title-{picture.Id}";
                    //     pictureService.UpdatePicture(picture);
                    // }


                    //
                    // pics = pictureService.GetBinaryLessPictures();
                    // if (!pics.Entity.Items.IsNullOrEmpty())
                    // {
                    //     var splashService = services.GetRequiredService<IUnsplashClient>();
                    //     var clientFactory = services.GetRequiredService<IHttpClientFactory>();
                    //     var client = clientFactory.CreateClient();
                    //     var pictures = splashService.GetRandomPicture();
                    //     foreach (var picture in pics.Entity.Items)
                    //     {
                    //         var index = pics.Entity.Items.ToList().IndexOf(picture) + 1;
                    //         var pictureIndex = index % 30;
                    //         if (index > 1 && pictureIndex == 1)
                    //         {
                    //             pictures = splashService.GetRandomPicture();
                    //         }
                    //
                    //         var response = client.GetAsync(pictures.ElementAt(pictureIndex).urls.raw)
                    //             .ConfigureAwait(true).GetAwaiter()
                    //             .GetResult();
                    //         using var content = response.Content.ReadAsStreamAsync().ConfigureAwait(true).GetAwaiter()
                    //             .GetResult();
                    //         using var image = Image.Load<Rgba32>(content, out var format);
                    //
                    //
                    //         picture.MimeType = format.DefaultMimeType;
                    //         picture.BinaryData = pictureService.EncodeImage(image, format, 75);
                    //         pictureService.UpdatePicture(picture);
                    //     }
                    // }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.Message, "An error occurred seeding the DB.");
                }
            }
        }
    }
}