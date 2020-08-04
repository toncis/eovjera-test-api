using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using eOvjera.Test.Model;
using eOvjera.Test.WebAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace eOvjera.Test.WebApi
{
    /// <summary>
    /// Web API - Startup init.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The enviroment.</param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets or sets the application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// Configures the services to add to the ASP.NET MVC 6 Injection of Control (IoC) container. This method gets
        /// called by the ASP.NET runtime. See
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCaching()
                .AddCustomOptions(this.Configuration)
                .AddCustomCors(this.Configuration)
                .AddJwtAuthenticationService(this.Configuration)
                .AddCustomIISOptions()
                .AddCustomMvc(this.Configuration)
                .AddCustomAuthorization()
                .AddSwagger(this.Configuration)
                .AddRepositories(this.Configuration)
                .AddServices(this.Configuration)
                .AddTranslators();
        }

        /// <summary>
        /// his method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var webApiEnviroment = this.Configuration.GetSection(WebApiValues.WebApiEnvirormentConfiguration).Get<EnvirormentSettings>();

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // Use HSTS Middleware (UseHsts) to send HTTP Strict Transport Security Protocol (HSTS) headers to clients if configuration requires..
                // Note : 
                //   Apps deployed in a reverse proxy configuration allow the proxy to handle connection security (HTTPS). 
                //   If the proxy also handles HTTPS redirection, there's no need to use HTTPS Redirection Middleware. 
                // Do not forget to :
                //  1. Set the ASPNETCORE_URLS environment variable.
                //  2. Set the ASPNETCORE_HTTPS_PORT environment variable or add a top-level entry in appsettings.json : "https_port": 443,.
                if(webApiEnviroment.WebApiEnforceHTTPS)
                    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
                });
            }

            // Use HTTPS Redirection Middleware (UseHttpsRedirection) to redirect HTTP requests to HTTPS if configuration requires.
            // Note : 
            //   Apps deployed in a reverse proxy configuration allow the proxy to handle connection security (HTTPS). 
            //   If the proxy also handles HTTPS redirection, there's no need to use HTTPS Redirection Middleware. 
            // Do not forget to :
            //  1. Set the ASPNETCORE_URLS environment variable.
            //  2. Set the ASPNETCORE_HTTPS_PORT environment variable or add a top-level entry in appsettings.json : "https_port": 443,.
            if(webApiEnviroment.WebApiEnforceHTTPS)
                app.UseHttpsRedirection(); 
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(WebApiValues.CorsPolicyAll);

            // app.UseAuthentication();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{webApiEnviroment.SwaggerEndpointBaseUrl}" } };
                });

                // Serialize Swagger in the 2.0 format
                // c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(webApiEnviroment.SwaggerEndpointUrl, webApiEnviroment.SwaggerEndpointName);
            });

        }
    }
}
