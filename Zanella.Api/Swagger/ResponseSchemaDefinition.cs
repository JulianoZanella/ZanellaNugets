namespace Zanella.Api.Swagger
{
    /// <summary>
    /// Default response definitions
    /// </summary>
    public class ResponseSchemaDefinition
    {
        /// <summary>
        /// Http Status Code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Type of response
        /// </summary>
        public Type Type { get; set; } = typeof(object);

        /// <summary>
        /// List of media types
        /// Default: "application/json"
        /// </summary>
        public IEnumerable<string>? MediaTypes { get; set; }
    }
}
