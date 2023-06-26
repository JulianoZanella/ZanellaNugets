using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Zanella.Api.Models;

namespace Zanella.Api.Extensions
{
    public static class ModelStateValidationExtensions
    {
        /// <summary>
        /// Validate Model State and return 422 if invalid
        /// </summary>
        /// <param name="services"></param>
        /// <param name="returnedContentTypes">Content types (default = application/problem+json)</param>
        /// <returns></returns>
        public static IServiceCollection AddModelStateValidation(this IServiceCollection services, MediaTypeCollection? returnedContentTypes = null)
        {
            return services.AddModelStateValidation<CustomProblemDetails, ErrorField>(returnedContentTypes);
        }

        /// <summary>
        /// Validate Model State and return 422 if invalid
        /// </summary>
        /// <typeparam name="T">Object with list of errors</typeparam>
        /// <typeparam name="TD">Error object</typeparam>
        /// <param name="services"></param>
        /// <param name="returnedContentTypes">Content types (default = application/problem+json)</param>
        /// <returns></returns>
        public static IServiceCollection AddModelStateValidation<T, TD>(this IServiceCollection services, MediaTypeCollection? returnedContentTypes = null)
            where T : IProblemDetails, new()
            where TD : IProblemDetail, new()
        {
            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var returned = new T
                    {
                        Errors = new List<IProblemDetail>(),
                    };

                    returned.Errors = (IEnumerable<IProblemDetail>)context.ModelState.Select(modelState =>
                    new TD
                    {
                        Field = modelState.Key,
                        Errors = modelState.Value.Errors.Select(x => x.ErrorMessage),
                    });

                    returnedContentTypes ??= new MediaTypeCollection { "application/problem+json" };

                    return new ObjectResult(returned)
                    {
                        ContentTypes = returnedContentTypes,
                        DeclaredType = typeof(T),
                        StatusCode = StatusCodes.Status422UnprocessableEntity,
                    };
                };
            });
        }
    }
}
