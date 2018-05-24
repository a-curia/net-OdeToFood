using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // we must register the custom services in here... like IGreeter
            services.AddSingleton<IGreeter, Greeter>();

            services.AddScoped<IRestaurantData, InMemoryRestaurantData>(); // http scoped lifetime


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

