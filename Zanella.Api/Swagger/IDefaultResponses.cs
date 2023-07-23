namespace Zanella.Api.Swagger
{
    /// <summary>
    /// Default Responses
    /// </summary>
    public interface IDefaultResponses
    {
        /// <summary>
        /// List of default responses to add
        /// </summary>
        IEnumerable<ResponseSchemaDefinition> ResponseSchemaDefinitions { get; }
    }
}
