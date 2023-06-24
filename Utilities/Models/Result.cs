using System.Collections.Generic;

namespace Utilities.Models
{
    public class Result
    {
        /// <summary>
        /// Success process
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Messages
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();

        public Result()
        {
        }

        public Result(string message) : this()
        {
            Messages.Add(message);
        }
    }

    public class Result<T> : Result
    {
        /// <summary>
        /// Value
        /// </summary>
        public T? Value { get; set; }

        public Result()
        {
        }

        public Result(string message) : this()
        {
            Messages.Add(message);
        }

        public static Result<T> Success(T value) => new()
        {
            IsSuccess = true,
            Value = value,
        };

        public static Result<T> Failure(string message) => new(message)
        {
            IsSuccess = false,
        };
    }
}
