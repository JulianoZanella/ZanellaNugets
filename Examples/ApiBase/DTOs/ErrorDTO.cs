namespace ApiBase.DTOs
{
    public class ErrorDTO
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Details
        /// </summary>
        public string? Details { get; set; }

        public ErrorDTO()
        {
        }

        public ErrorDTO(string message, string? details = null) : this()
        {
            Message = message;
            Details = details;
        }
    }
}
