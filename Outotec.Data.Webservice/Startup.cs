using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Outotec.Data.Webservice.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Outotec.Data.Webservice
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            // URL: /swagger
            app.UseSwaggerUI(options =>
            {
                //options.DocExpansion("none");
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Outotec Swagger API v1");
            });
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
            services.AddMvc();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Outotec Swagger API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            // Set up the AutoFac container for Dependency Injection.
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddMediatRUsingAutoFac();

            RegisterServices(builder);

            // Create the IServiceProvider based on the container.
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            // When/if you create any classes you want to inject using DI, register them here.
            // There are various ways, but one is: builder.RegisterType<T>();
            // If you intend for it to be a singleton (persisting service/helper class), do this: builder.RegisterType<T>().SingleInstance();
        }
    }
}