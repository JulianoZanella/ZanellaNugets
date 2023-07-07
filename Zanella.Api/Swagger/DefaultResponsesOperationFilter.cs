using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Reflection;

namespace Zanella.Api.Swagger
{
    public class DefaultResponsesOperationFilter : IOperationFilter
    {
        private readonly IDefaultResponses _defaultResponses;

        public DefaultResponsesOperationFilter(IDefaultResponses defaultResponses)
        {
            _defaultResponses = defaultResponses;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var item in _defaultResponses.ResponseSchemaDefinitions)
            {
                if (context.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>() != null
                    && (item.StatusCode == (int)HttpStatusCode.Unauthorized || item.StatusCode == (int)HttpStatusCode.Forbidden)
                    )
                {
                    continue;
                }

                AddResponse(operation, context, item);
            }
        }

        private static void AddResponse(OpenApiOperation operation, OperationFilterContext context
            , ResponseSchemaDefinition responseSchemaDefinition)
        {
            if (operation.Responses.Any(x => x.Key == responseSchemaDefinition.StatusCode.ToString()))
                return;

            // Try to get my model from swagger schema repository
            context.SchemaRepository.TryLookupByType(responseSchemaDefinition.Type, out var schema);

            // Add schema inside if to exclude multiple adding situation
            schema ??= context.SchemaGenerator.GenerateSchema(responseSchemaDefinition.Type, context.SchemaRepository);

            var listOfResponses = responseSchemaDefinition.MediaTypes?.Distinct();
            if (listOfResponses == null || !listOfResponses.Any())
                listOfResponses = new List<string> { "application/json" };

            var content = new Dictionary<string, OpenApiMediaType>();
            foreach (var item in listOfResponses)
            {
                content.Add(item, new OpenApiMediaType { Schema = schema });
            }

            // Add response
            operation.Responses.Add(responseSchemaDefinition.StatusCode.ToString(), new OpenApiResponse
            {
                Content = content,
                Description = responseSchemaDefinition.Description,
            });
        }
    }
}
