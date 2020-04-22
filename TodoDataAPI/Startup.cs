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
using Microsoft.AspNetCore.Cors.Infrastructure;

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

			services.AddTransient(typeof(DevUsersRegistration));
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
						.AllowAnyMethod()
						.AllowCredentials().AllowAnyOrigin()
						.SetIsOriginAllowed((host) => true)
						.AllowAnyHeader());
			});

			services.AddOData();


			services.AddMvc(opt =>
			{

			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

		}


		public void Configure(IApplicationBuilder app, IHostingEnvironment env, TodoContext context, DevUsersRegistration reg)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			context.Database.EnsureCreated();

			app.UseHttpsRedirection();

			app.UseCors("CorsPolicy");


			app.UseAuthentication();

			reg.RegisterIfNotPresent().GetAwaiter().GetResult();



			app.UseMvc(routeBuilder =>
			{

				routeBuilder.EnableDependencyInjection();

				routeBuilder.Expand().Select().OrderBy().Filter();

			});

		}
	}
	public class DevUsersRegistration
	{

		private readonly UserManager<AppUser> _manager;
		public DevUsersRegistration(UserManager<AppUser> manager) => (this._manager) = (manager);



		internal async Task RegisterIfNotPresent()
		{
			if (!_manager.Users.Any())
			{
				var user = new AppUser { UserName = "dev", Email = "dev@todo.me" };
				var result = await _manager.CreateAsync(user, "pass1234");

				if (!result.Succeeded)
				{
					Trace.WriteLine(result.Errors);
				}

			}
		}
	}
}
