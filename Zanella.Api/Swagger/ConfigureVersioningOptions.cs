﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zanella.Api.Swagger
{
    /// <summary>
    /// Versioning Options
    /// </summary>
    public class ConfigureVersioningOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IApiInfoProvider _apiInfoProvider;

        /// <summary>
        /// Versioning Options
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="apiInfoProvider"></param>
        public ConfigureVersioningOptions(IApiVersionDescriptionProvider provider, IApiInfoProvider apiInfoProvider)
        {
            _provider = provider;
            _apiInfoProvider = apiInfoProvider;
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var defaultInfo = _apiInfoProvider.DefaultOpenApiInfo ?? new OpenApiInfo();
            var info = new OpenApiInfo()
            {
                Title = defaultInfo.Title,
                Version = description.ApiVersion.ToString(),
                Description = defaultInfo.Description,
                Contact = defaultInfo.Contact,
                Extensions = defaultInfo.Extensions,
                License = defaultInfo.License,
                TermsOfService = defaultInfo.TermsOfService,
            };

            if (description.IsDeprecated)
                info.Description = _apiInfoProvider.DefaultDescriptionIfDeprecated();

            return info;
        }
    }
}
