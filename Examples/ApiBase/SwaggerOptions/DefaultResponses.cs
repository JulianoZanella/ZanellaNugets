using ApiBase.DTOs;
using System.Net;
using Zanella.Api.Models;
using Zanella.Api.Swagger;

namespace ApiBase.SwaggerOptions
{
    public class DefaultResponses : IDefaultResponses
    {
        public IEnumerable<ResponseSchemaDefinition> ResponseSchemaDefinitions => new[]
        {
            new ResponseSchemaDefinition()
            {
                Description = "Bad Request",
                StatusCode = (int)HttpStatusCode.BadRequest,
                Type = typeof(ErrorDTO),
            },
            new ResponseSchemaDefinition()
            {
                Description = "Unauthorized",
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Type = typeof(ErrorDTO),
            },
            new ResponseSchemaDefinition()
            {
                Description = "Validation Error",
                StatusCode = 422,
                Type = typeof(CustomProblemDetails),
                MediaTypes = new string[]
                {
                    "application/problem+json"
                },
            },
            new ResponseSchemaDefinition()
            {
                Description = "Internal Server Error",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Type = typeof(ErrorDTO),
            },
        };
    }
}
