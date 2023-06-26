namespace Zanella.Api.Models
{
    /// <summary>
    /// Errors in validation
    /// </summary>
    public class CustomProblemDetails : IProblemDetails
    {
        /// <summary>
        /// List of Errors
        /// </summary>
        public IEnumerable<IProblemDetail> Errors { get; set; } = new List<ErrorField>();
    }

    /// <summary>
    /// Errors in validation
    /// </summary>
    public class ErrorField : IProblemDetail
    {
        /// <summary>
        /// Name of property
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// List of error messages
        /// </summary>
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
