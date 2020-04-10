using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Net;
using TodoDataAPI.Models;
using System.Diagnostics;

namespace TodoDataAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {


            

            services.AddDbContext<TodoContext>(options =>
            {
                Trace.WriteLine(Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]);
            }
            );
            services.AddIdentity<AppUser, IdentityRole>(config =>
            {

                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<TodoContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Events.OnRedirectToLogin = ctx =>
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Task.FromResult(0);

                };
                config.LoginPath = "/api/auth/login";

            });

           


            services.AddOData();


            services.AddMvc(opt =>
            {

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TodoContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            context.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


            app.UseAuthentication();





            app.UseMvc(routeBuilder =>
            {

                routeBuilder.EnableDependencyInjection();

                routeBuilder.Expand().Select().OrderBy().Filter();

            });

        }
    }
}
