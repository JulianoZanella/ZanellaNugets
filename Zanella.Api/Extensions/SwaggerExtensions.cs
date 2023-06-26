using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Zanella.Api.Swagger;

namespace Zanella.Api.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Add default versions options to swagger
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerVersioning<T>(this IServiceCollection services)
            where T : class, IApiInfoProvider
        {
            services.AddTransient<IApiInfoProvider, T>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureVersioningOptions>();
            return services;
        }

        /// <summary>
        /// Add default descriptions for versioning
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SwaggerGenOptions AddVersionDefaultValues(this SwaggerGenOptions options)
        {
            options.OperationFilter<SwaggerDefaultValues>();
            return options;
        }

        /// <summary>
        /// Add JWT token defaults
        /// </summary>
        /// <param name="options"></param>
        /// <param name="description">A short description</param>
        /// <param name="parameterLocation"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static SwaggerGenOptions AddJWTSecurity(this SwaggerGenOptions options
            , string description
            , ParameterLocation parameterLocation = ParameterLocation.Header
            , string parameterName = "Authorization"
        )
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = description,
                Name = parameterName,
                In = parameterLocation,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            return options;
        }

        /// <summary>
        /// Add version endpoints on UI
        /// </summary>
        /// <param name="options"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        /// <returns></returns>
        public static SwaggerUIOptions AddVersionEndpoints(this SwaggerUIOptions options, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (apiVersionDescriptionProvider == null)
                return options;

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }

            return options;
        }
    }
}
