using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        // add a constructor; constructor for this class is injectable
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // we must register the custom services in here... like IGreeter
            services.AddSingleton<IGreeter, Greeter>();

            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>(); // http scoped lifetime - instanciate an instance for each http request, reuse that instance through out of the request and then throw it away
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>(); //because i want to see the changes on different requests

            services.AddDbContext<OdeToFoodDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("OdeToFood"))
            );
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            /*
             Entity Framework Migrations
	            dotnet ef migrations add
	            dotnet ef database update
	            dotnet ef context list
	            dotnet ef context info

	            dotnet ef migrations add --help
	            dotnet ef migrations add InitialCreate -v
	
	            dotnet ef database --help
	            dotnet ef database update -v
             */

            // setting up the ASP.NET MVC framework
            // step 1 - add the package dependency - do it if not done by default
            // step 2 - this one - add the MVC service
            // step 3 - add the MVC middleware
            services.AddMvc();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IHostingEnvironment env,
                                IGreeter greeter,
                                ILogger<Startup> logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // allow all requests to flow through but raises on exception and shows exception details for developer
            }

            // Serving static files
            app.UseStaticFiles();


            //Routing
            // Convention Based Routing - in Startup
            // routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            // *
            // Attribute Based Routing
            // [Route("[controller]/[action]")]


            app.UseMvc(ConfigureRoutes);

  


            // this runs for every request that we receive
            app.Run(async (context) =>
            {
                var customGreeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Not found");
            });


        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /admin/Home/Index/4
            //routeBuilder.MapRoute("Default", "admin/{controller=Home}/{action=Index}/{id?}"); //? means the id is optional; if you do not see the controller name use HomeController; same for action method
            //throw new NotImplementedException();

            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");

        }
    }
}

