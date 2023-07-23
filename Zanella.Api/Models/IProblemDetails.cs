namespace Zanella.Api.Models
{
    /// <summary>
    /// Validation errors
    /// </summary>
    public interface IProblemDetails
    {
        /// <summary>
        /// List of Errors
        /// </summary>
        public IEnumerable<IProblemDetail> Errors { get; set; }
    }
}
