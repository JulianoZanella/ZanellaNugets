# Zanella.Utilities

Contains basic utilities for API projects

## Dependencies

[Asp.Versioning.Mvc.ApiExplorer 7.0.0](https://www.nuget.org/packages/Asp.Versioning.Mvc.ApiExplorer/)
[Swashbuckle.AspNetCore.SwaggerGen 6.5.0](https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerGen)
[Swashbuckle.AspNetCore.SwaggerUI 6.5.0](https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerGen)

## Installation

Use [nuget manager](https://www.nuget.org/packages/Zanella.Api/) to install.

## Usage

```csharp

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


public class ApiInfoProvider : IApiInfoProvider
{
    public OpenApiInfo DefaultOpenApiInfo => new OpenApiInfo()
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

// class Program.cs
using Zanella.Api.Extensions;

/// ...
// Add versioning
var versioningBuilder = builder.Services.AddApiVersioning(p =>
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
// Add filter for model state validation
builder.Services.AddModelStateValidation();
// Add swagger
builder.Services.AddEndpointsApiExplorer();
// Add versioning for swagger
builder.Services.AddSwaggerVersioning<ApiInfoProvider>();
// Add common default responses
builder.Services.AddSwaggerDefaultResponses<DefaultResponses>();
builder.Services.AddSwaggerGen(options =>
{
    // Add versioning
    options.AddVersionDefaultValues();
    // Add default responses
    options.AddDefaultResponses();
    // Add JWT header
    options.AddJWTSecurity("JWT Authorization Header");
});

/// ...
// Use swagger with versioning
app.UseSwagger();
var apiVersionDescriptionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
app.UseSwaggerUI(options =>
{
    options.AddVersionEndpoints(apiVersionDescriptionProvider);
});

```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
