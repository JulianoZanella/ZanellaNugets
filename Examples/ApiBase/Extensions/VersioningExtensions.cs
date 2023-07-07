using Asp.Versioning;

namespace ApiBase.Extensions
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            var versioningBuilder = services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(2, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            versioningBuilder.AddApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
