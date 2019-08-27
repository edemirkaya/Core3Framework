using System;
using CommonCore.Configurations;
using CommonCore.ExceptionHandling;
using CommonCore.ExceptionHandling.Interfaces;
using CommonCore.Interfaces;
using CommonCore.Logging;
using CommonCore.Server.Services;
using Core3_Framework.Business.Infrastructure;
using Core3_Framework.Business.Infrastructure.Settings;
using Core3_Framework.Business.Services;
using Core3_Framework.Contracts.Services;
using Core3_Framework.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace App.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDb>(ctx =>
            {
                try
                {
                    ctx.UseNpgsql(Configuration.GetConnectionString("IRes"));
                    //ctx.UseNpgsql(Configuration.GetConnectionString("IRes"), b => b.MigrationsAssembly("App.WebAPI"));
                }
                catch (Exception)
                {

                    throw;
                }
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICacheItemService, CacheItemService>();

            // setup JWT parameters
            services.Configure<JwtIssuerSettings>(Configuration.GetSection(nameof(JwtIssuerSettings)));
            services.AddTransient<IJwtIssuerOptions, JwtIssuerFactory>();

            // setup JWT Token validation
            services.Configure<JwtTokenValidationSettings>(Configuration.GetSection(nameof(JwtTokenValidationSettings)));
            services.AddSingleton<IJwtTokenValidationSettings, JwtTokenValidationSettingsFactory>();

            // Create TokenValidation factory with DI priciple
            var tokenValidationSettings = services.BuildServiceProvider().GetService<IJwtTokenValidationSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationSettings.CreateTokenValidationParameters();
                    options.SaveToken = true;
                });

            // Secure all controllers by default
            var authorizePolicy = new AuthorizationPolicyBuilder()
                                                        .RequireAuthenticatedUser()
                                                        .Build();

            //Exception handling manager
            services.AddScoped<IExceptionHandlingManager, ExceptionHandlingManager>();

            //Cache
            services.AddScoped<ICacheManager, CacheManager>();
            services.AddMemoryCache();

            //RolService
            services.AddScoped<IRoleService, RoleService>();

            ////Mapping
            var m_config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new ModelMapping());
                //cfg.AddProfiles(typeof(ModelMapping).GetTypeInfo().Assembly);
            });
            var mapper = m_config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().AddNewtonsoftJson();

            services.AddSingleton<ILogHelper, LoggerSource>();
            // Add Mvc with options
            services
                    .AddMvc(config =>
                    {
                        config.Filters.Add(new AuthorizeFilter(authorizePolicy));
                        config.EnableEndpointRouting = false;
                    })
                    // Override default camelCase style (yes its strange the default configuration results in camel case)
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserResolverService>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               
                endpoints.MapRazorPages();
                
            });
        }
    }
}
