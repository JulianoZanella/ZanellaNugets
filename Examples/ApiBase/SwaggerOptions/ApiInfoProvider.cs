using Microsoft.OpenApi.Models;
using Zanella.Api.Swagger;

namespace ApiBase.SwaggerOptions
{
    public class ApiInfoProvider : IApiInfoProvider
    {
        public OpenApiInfo DefaultOpenApiInfo => new()
        {
            Title = "API Versioning and Swagger",
            Description = "ApiVersioningSwagger",
            Contact = new OpenApiContact()
            {
                Name = "Administrator",
                Email = "admin@email.com"
            }
        };

        public string DefaultDescriptionIfDeprecated()
        {
            return DefaultOpenApiInfo.Description + "<br/> This API version has been deprecated.";
        }
    }
}
