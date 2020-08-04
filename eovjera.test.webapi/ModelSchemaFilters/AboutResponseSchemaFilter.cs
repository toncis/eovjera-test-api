using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using eOvjera.Test.WebAPI.Models;
// using eOvjera.Test.WebAPI.ViewModels;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace eOvjera.Test.WebAPI.ViewModelschemaFilters
{
    /// <summary>
    /// Web API - View - Api about response schema filter.
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter" />
    public class AboutResponseSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            model.Example = new OpenApiObject()
            {
                [ "application" ] = new OpenApiString(@"eOvjera TEST WebAPI"),
                [ "version" ] = new OpenApiString(@"v1.0"),
                [ "date" ] = new OpenApiDateTime(new DateTimeOffset(DateTime.Now)),
                [ "os" ] = new OpenApiString(@"Linux 4.15.0-58-generic"),
                [ "framework" ] = new OpenApiString(@".NET Core  3.1.302"),
                [ "workingFolder" ] = new OpenApiString(@"/var/www/eOvjera/eovjera_test_webapi")
            };
        }
    }
}