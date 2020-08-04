using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using System.Linq;
using eOvjera.Common.Cache;
using eOvjera.Test.Model;
using eOvjera.Test.WebAPI.Constants;

namespace eOvjera.Test.WebAPI
{
    /// <summary>
    /// StartUp - Service Collection Extensions.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures caching for the application. Registers the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> and
        /// <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> types with the services collection or IoC container. The
        /// <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> is intended to be used in cloud hosted scenarios where there is a shared
        /// cache, which is shared between multiple instances of the application. Use the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/>
        /// otherwise.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            return services
                // Adds IMemoryCache which is a simple in-memory cache.
                .AddMemoryCache()
                // Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
                // default implementation of IDistributedCache which is not distributed. See below:
                .AddDistributedMemoryCache()
                // Uncomment the following line to use the Redis implementation of IDistributedCache. This will
                // override any previously registered IDistributedCache service.
                // Redis is a very fast cache provider and the recommended distributed cache provider.
                // .AddDistributedRedisCache(
                //     options =>
                //     {
                //     });
                // Uncomment the following line to use the Microsoft SQL Server implementation of IDistributedCache.
                // Note that this would require setting up the session state database.
                // Redis is the preferred cache implementation but you can use SQL Server if you don't have an alternative.
                // .AddSqlServerCache(
                //     x =>
                //     {
                //         x.ConnectionString = "Server=.;Database=ASPNET5SessionState;Trusted_Connection=True;";
                //         x.SchemaName = "dbo";
                //         x.TableName = "Sessions";
                //     });
                //
                // Add my custom memory cache with Dictionary of cached objects
                .AddSingleton<IMyMemoryCache, MyMemoryCache>()                ;
        }

        /// <summary>
        /// Configures the settings by binding the contents of the config.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding objects to the services collection.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                // Adds IOptions<CacheProfileSettings> to the services container.
                // .Configure<AuthAudience>(configuration.GetSection(WebApiValues.ConfigSectionJwt))
                // .Configure<CacheProfileSettings>(configuration.GetSection(nameof(CacheProfileSettings)))
                // .Configure<IeOvjera.Model.LdapSettings>(configuration.GetSection(WebApiValues.ConfigSectionLdapSettings))
                // .Configure<DMSSettings>(configuration.GetSection(WebApiValues.ConfigSectionDMSSettings))
                .Configure<EnvirormentSettings>(configuration.GetSection(WebApiValues.ConfigSectionApiEnvirormentSettings))
                .AddSingleton<IConfiguration>(configuration)
                ;
        }


        /// <summary>
        /// Adds custom routing preferences.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCustomRouting(this IServiceCollection services)
        {
            return services.AddRouting(
                options =>
                {
                    // Improve SEO by stopping duplicate URL's due to case differences or trailing slashes.
                    // See http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
                    // All generated URL's should append a trailing slash. Defalut = false.
                    options.AppendTrailingSlash = true;
                    // All generated URL's should be lower-case. Defalut = false.
                    options.LowercaseUrls = false;
                });
        }

        /// <summary>
        /// Add Custom MVC options, JSON options to services .
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        public static IServiceCollection AddCustomMvc(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddControllers()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                // .AddNewtonsoftJson(
                //     options =>
                //     {
                //         options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //         //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                //         options.SerializerSettings.DateFormatString = "dd.MM.yyyy. HH:mm:ss";
                //     })
                ;

            return services;
        }

        /// <summary>
        /// Add JWT Authentication to services .
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        public static IServiceCollection AddJwtAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            // var audienceConfig = configuration.GetSection(WebApiValues.ConfigSectionJwt);
            // var symmetricKeyAsBase64 = audienceConfig["Secret"];
            // var keyByteArray = System.Text.Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            // var signingKey = new SymmetricSecurityKey(keyByteArray);

            // var tokenValidationParameters = new TokenValidationParameters
            // {
            //     // The signing key must match!
            //     ValidateIssuerSigningKey = true,
            //     IssuerSigningKey = signingKey,

            //     // Validate the JWT Issuer (iss) claim
            //     ValidateIssuer = true,
            //     ValidIssuer = audienceConfig["Iss"],

            //     // Validate the JWT Audience (aud) claim
            //     ValidateAudience = true,
            //     ValidAudience = audienceConfig["Aud"],

            //     // Validate the token expiry
            //     ValidateLifetime = true,

            //     ClockSkew = System.TimeSpan.Zero
            // };

            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // })
            // //.AddJwtBearerAuthentication(o =>
            // //{
            // //    o.Authority = "";
            // //    //o.Audience = "";
            // //    o.TokenValidationParameters = tokenValidationParameters;
            // //})
            // .AddJwtBearer(o =>
            // {
            //     //o.Authority = "";
            //     //o.Audience = "";
            //     o.TokenValidationParameters = tokenValidationParameters;
            // });

            return services;
        }

        /// <summary>
        /// Add IIS Options for hosting .NET Core app on MS Windows Server IIS.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCustomIISOptions(this IServiceCollection services)
        {
            return services.Configure<IISOptions>(options => {
                options.AutomaticAuthentication = true;
            });
        }

        /// <summary>
        /// Adds customized JSON serializer settings.
        /// </summary>
        /// <param name="builder">The builder used to configure MVC services.</param>
        public static IMvcCoreBuilder AddCustomJsonOptions(this IMvcCoreBuilder builder)
        {
            return null;
            
            // return builder.AddNewtonsoftJson(
            //     options =>
            //     {
            //         // Parse dates as DateTimeOffset values by default. You should prefer using DateTimeOffset over
            //         // DateTime everywhere. Not doing so can cause problems with time-zones.
            //         options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
            //         // Output enumeration values as strings in JSON.
            //         options.SerializerSettings.Converters.Add(new StringEnumConverter());
            //     });
        }

        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            // .NET Core v2.2, v3
            var apiCorsConfig = configuration.GetSection(WebApiValues.ConfigSectionCors).GetChildren();

            if(!apiCorsConfig.Any(c => c.GetValue<string>("CorsPolicyName") == WebApiValues.CorsPolicyAll))
            {
                services.AddCors(o => {
                    // [ Add CORS policy for all ]
                    o.AddPolicy(
                        WebApiValues.CorsPolicyAll,
                        builder =>
                        {
                            builder.WithOrigins(
                                    "https://localhost", 
                                    "http://localhost", 
                                    "https://localhost:4200", 
                                    "http://localhost:4200", 
                                    "https://10.201.*.*", 
                                    "http://10.201.*.*")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });

                });
            }

            foreach (var corsConfig in apiCorsConfig)
            {
                var corsName = corsConfig["CorsPolicyName"];
                var corsEnabled = corsConfig.GetValue<bool>("Enabled");
                var urlOrigins = corsConfig.GetSection("Origins").GetChildren().Select(c => c.Value).ToArray();

                if(corsEnabled == true)
                {
                    services.AddCors(o => {
                        o.AddPolicy(
                            corsName,
                            builder =>
                            {
                                builder.WithOrigins(urlOrigins)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                            });

                    });
                }
            }
            
            return services;
        }

        /// <summary>
        /// Add authorization policy. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            // Use policy auth.
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "WfMSuperUser",
                    policy => policy.RequireClaim(ClaimTypes.Role, CustomClaimValues.SuperUser));
                options.AddPolicy(
                    "WfMAdminUser",
                    policy => policy.RequireClaim(ClaimTypes.Role, CustomClaimValues.AdminUser));
                options.AddPolicy(
                    "WfMUser",
                    policy => policy.RequireClaim(ClaimTypes.Role, CustomClaimValues.User));
            });

            return services;
        }

        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var webApiEnviroment = configuration.GetSection(WebApiValues.WebApiEnvirormentConfiguration).Get<EnvirormentSettings>();

            services.AddSwaggerGen(
                options =>
                {
                     //var assembly = typeof(Startup).GetTypeInfo().Assembly;
                     //var strTitle = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                     //var strDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    var strAppContact = new OpenApiContact() 
                    { 
                        Email = webApiEnviroment.WebApiContactMail, 
                        Name = webApiEnviroment.WebApiContactName, 
                        Url = new System.Uri(webApiEnviroment.WebApiContactUrl)
                    };

                    var strAppLicence = new OpenApiLicense() 
                    { 
                        Name = webApiEnviroment.WebApiLicenseName, 
                        Url = new System.Uri(webApiEnviroment.WebApiLicenseUrl) 
                    };                        

                    options.EnableAnnotations();

                     //// Add the XML comment file for this assembly, so it's contents can be displayed.
                    if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        // Use windows path with \\
                        options.IncludeXmlComments(webApiEnviroment.SwaggerXmlComments);
                    else
                        // Use linux path with \
                        options.IncludeXmlComments(webApiEnviroment.SwaggerXmlComments.Replace(@"\", @"/"));

                     //// Show an example model for JsonPatchDocument<T>.
                     //options.SchemaFilter<JsonPatchDocumentSchemaFilter>();
                     //// Show an example model for ModelStateDictionary.
                     //options.SchemaFilter<ModelStateDictionarySchemaFilter>();

                     options.SwaggerDoc(
                        "v1",
                        new OpenApiInfo()
                        {
                            Version = webApiEnviroment.WebApiVersion,
                            Title = webApiEnviroment.WebApiTitle,
                            Description = webApiEnviroment.WebApiDescription,
                            Contact = strAppContact,
                            License = strAppLicence,
                            TermsOfService = new System.Uri(webApiEnviroment.WebApiLicenseUrl) // webApiEnviroment.WebApiTermOfService,
                        });


                    options.CustomSchemaIds(x => x.FullName);
                });


            return services;
        }

        /// <summary>
        /// Adds project commands.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            // To Do
            return services;
            // return services
            //     .AddScoped<IDeleteCarCommand, DeleteCarCommand>()
            //     .AddScoped(x => new Lazy<IDeleteCarCommand>(() => x.GetRequiredService<IDeleteCarCommand>()))
            //     .AddScoped<IGetCarCommand, GetCarCommand>()
            //     .AddScoped(x => new Lazy<IGetCarCommand>(() => x.GetRequiredService<IGetCarCommand>()))
            //     .AddScoped<IGetCarPageCommand, GetCarPageCommand>()
            //     .AddScoped(x => new Lazy<IGetCarPageCommand>(() => x.GetRequiredService<IGetCarPageCommand>()))
            //     .AddScoped<IPatchCarCommand, PatchCarCommand>()
            //     .AddScoped(x => new Lazy<IPatchCarCommand>(() => x.GetRequiredService<IPatchCarCommand>()))
            //     .AddScoped<IPostCarCommand, PostCarCommand>()
            //     .AddScoped(x => new Lazy<IPostCarCommand>(() => x.GetRequiredService<IPostCarCommand>()))
            //     .AddScoped<IPutCarCommand, PutCarCommand>()
            //     .AddScoped(x => new Lazy<IPutCarCommand>(() => x.GetRequiredService<IPutCarCommand>()));

            // Singleton - Only one instance is ever created and returned.
            // services.AddSingleton<IExampleService, ExampleService>();

            // Scoped - A new instance is created and returned for each request/response cycle.
            // services.AddScoped<IExampleService, ExampleService>();

            // Transient - A new instance is created and returned each time.
            // services.AddTransient<IExampleService, ExampleService>();
        }

        /// <summary>
        /// Adds project DB Repositories.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // To Do
            return services;
        }

        /// <summary>
        /// Adds project WfM services.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add all BAL services
            return services;
        //     // Add all BAL services
        //      .AddTransient<ILdapService, LdapService>()
        //      .AddTransient<IWordDocumentAssemblerService, WordDocumentAssemblerService>()
        //      .AddTransient<IPdfDocumentAssemblerService, PdfDocumentAssemblerService>()
        //      .AddTransient<IDocumentAssemblyService, DocumentAssemblyService>()

        //    //dms service
        //     .AddScoped<IDocumentTemplateService, DocumentTemplateService>()
        //     .AddScoped<IDMSService, DMSService>()
        //     // Email configuration and service
        //     .AddSingleton<IEmailConfiguration>(configuration.GetSection(WebApiValues.ConfigSectionEmail).Get<EmailConfiguration>())
        //     .AddScoped<IEmailService, MailKitEmailService>();
        }

        /// <summary>
        /// Adds project translators.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddTranslators(this IServiceCollection services)
        {
            return services;
            // Add all translators
            // .AddSingleton<IMapper<VwWfInstanceStepCurrent, WorkflowSearchResultResponse>, WorkflowSearchResultMapperMapper>();
        }

    }
}
