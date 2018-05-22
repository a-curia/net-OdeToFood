using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IHostingEnvironment env,
                                IGreeter greeter,
                                ILogger<Startup> logger)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage(); // allow all requests to flow through but raises on exception and shows exception details for developer
            //}



            // this runs for every request that we receive
            app.Run(async (context) =>
            {

                throw new Exception("error received!");

                var customGreeting = greeter.GetMessageOfTheDay();
                await context.Response.WriteAsync(customGreeting);
            });


        }
    }
}

