using System.Collections.Generic;

namespace Zanella.Utilities.Models
{
    /// <summary>
    /// Result
    /// </summary>
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

        /// <summary>
        /// Result
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Result
        /// </summary>
        /// <param name="message"></param>
        public Result(string message) : this()
        {
            Messages.Add(message);
        }
    }

    /// <summary>
    /// Result
    /// </summary>
    /// <typeparam name="T">Type of Value</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Value
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        /// Result
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Result
        /// </summary>
        /// <param name="message"></param>
        public Result(string message) : this()
        {
            Messages.Add(message);
        }

        /// <summary>
        /// Create Success Result
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Success(T value) => new()
        {
            IsSuccess = true,
            Value = value,
        };

        /// <summary>
        /// Create Failure Result
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<T> Failure(string message) => new(message)
        {
            IsSuccess = false,
        };
    }
}
