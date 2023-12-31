﻿using ApiBase.SwaggerOptions;
using Asp.Versioning.ApiExplorer;
using Zanella.Api.Extensions;

namespace ApiBase.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerVersioning<ApiInfoProvider>();
            services.AddSwaggerDefaultResponses<DefaultResponses>();

            var hostEnvironment = services.BuildServiceProvider().GetRequiredService<IHostEnvironment>();

            services.AddSwaggerGen(options =>
            {
                options.AddVersionDefaultValues();
                options.AddDefaultResponses();

                options.AddJWTSecurity("JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                    "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                    "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'");
                if (hostEnvironment != null)
                {
                    // integrate xml comments
                    foreach (var item in GetXmlCommentsFilePaths(hostEnvironment))
                    {
                        options.IncludeXmlComments(item);
                    }
                }
            });

            return services;
        }

        private static IEnumerable<string> GetXmlCommentsFilePaths(IHostEnvironment hostEnvironment)
        {
            var basePath = hostEnvironment.ContentRootPath;
            return Directory.EnumerateFiles(basePath, "*.xml", SearchOption.AllDirectories);
        }

        public static WebApplication UseSwaggerCustom(this WebApplication app)
        {
            app.UseSwagger();

            var apiVersionDescriptionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();

            app.UseSwaggerUI(options =>
            {
                if (apiVersionDescriptionProvider != null)
                    options.AddVersionEndpoints(apiVersionDescriptionProvider);
            });
            return app;
        }
    }
}
