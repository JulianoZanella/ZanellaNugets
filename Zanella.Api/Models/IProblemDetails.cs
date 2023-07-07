namespace Zanella.Api.Models
{
    public interface IProblemDetails
    {
        /// <summary>
        /// List of Errors
        /// </summary>
        public IEnumerable<IProblemDetail> Errors { get; set; }
    }
}
