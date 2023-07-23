namespace Zanella.Api.Models
{
    /// <summary>
    /// Validation error
    /// </summary>
    public interface IProblemDetail
    {
        /// <summary>
        /// Name of property
        /// </summary>
        string Field { get; set; }

        /// <summary>
        /// List of error messages
        /// </summary>
        IEnumerable<string> Errors { get; set; }
    }
}
