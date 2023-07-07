namespace ApiBase.DTOs
{
    public class ResultDTO
    {
        public bool IsSuccess { get; set; }

        public string? Error { get; set; }

        public static ResultDTO Success() => new()
        {
            IsSuccess = true,
        };

        public static ResultDTO Failure(string error) => new()
        {
            IsSuccess = false,
            Error = error,
        };
    }

    public class ResultDTO<T>
    {
        public bool IsSuccess { get; set; }

        public T? Value { get; set; }

        public string? Error { get; set; }

        public static ResultDTO<T> Success(T value) => new()
        {
            Value = value,
            IsSuccess = true,
        };

        public static ResultDTO<T> Failure(string error) => new()
        {
            IsSuccess = false,
            Error = error,
        };
    }
}
