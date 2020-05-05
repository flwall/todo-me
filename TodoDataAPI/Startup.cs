
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoDataAPI.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

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
                    .SetIsOriginAllowed(_=>true)
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader());
            });

            services.AddMvc(opt =>
            {


            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../todo-front/dist";
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TodoContext context, DevUsersRegistration reg)
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

            if (env.IsDevelopment())
            {
                app.UseCors("CorsPolicy");
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            reg.RegisterIfNotPresent().GetAwaiter().GetResult();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(options =>
            {
                options.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../todo-front";


                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
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
