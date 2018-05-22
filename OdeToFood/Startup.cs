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
            //    app.UseDeveloperExceptionPage();
            //}

            // delegates and the func type
            // app.Use - write lower level middleware

            app.Use(next => //allow next piece of middlevare to process this request
            {
                return async context => //this is inwoked onece per http request
                {
                    logger.LogInformation("Request incoming!");
                    if (context.Request.Path.StartsWithSegments("/mym"))
                    {
                        await context.Response.WriteAsync("Hit!");
                        logger.LogInformation("Request handled!");
                    }
                    else // let the next middleware chance to respond
                    {
                        await next(context);
                        logger.LogInformation("Response outgoind!"); // this is the control flow back of the pipeline
                    }
                };
            });

            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/wp"
            });

            // this runs for every request that we receive
            app.Run(async (context) =>
            {
                var customGreeting = greeter.GetMessageOfTheDay();
                await context.Response.WriteAsync(customGreeting);
            });


        }
    }
}

