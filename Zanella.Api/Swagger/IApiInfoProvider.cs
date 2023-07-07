using Microsoft.OpenApi.Models;

namespace Zanella.Api.Swagger
{
    public interface IApiInfoProvider
    {
        OpenApiInfo DefaultOpenApiInfo { get; }

        string DefaultDescriptionIfDeprecated();
    }
}
