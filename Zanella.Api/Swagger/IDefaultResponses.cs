namespace Zanella.Api.Swagger
{
    public interface IDefaultResponses
    {
        IEnumerable<ResponseSchemaDefinition> ResponseSchemaDefinitions { get; }
    }
}
