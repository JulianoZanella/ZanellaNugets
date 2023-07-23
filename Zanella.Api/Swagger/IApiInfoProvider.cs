using Microsoft.OpenApi.Models;

namespace Zanella.Api.Swagger
{
    /// <summary>
    /// Api Info Provider
    /// </summary>
    public interface IApiInfoProvider
    {
        /// <summary>
        /// Default API Info
        /// </summary>
        OpenApiInfo DefaultOpenApiInfo { get; }

        /// <summary>
        /// Sets message to deprecated
        /// </summary>
        /// <returns></returns>
        string DefaultDescriptionIfDeprecated();
    }
}
