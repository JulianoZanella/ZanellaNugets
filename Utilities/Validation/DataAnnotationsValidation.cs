﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Utilities.Models;

namespace Utilities.Validation
{
    public class DataAnnotationsValidation
    {
        /// <summary>
        /// Validate Data Annotation Attributes
        /// </summary>
        /// <param name="item"></param>
        /// <param name="validationResults"></param>
        /// <param name="messageWhenNull"></param>
        /// <returns></returns>
        public bool ValidateObject(object item, out List<ValidationResult> validationResults, string messageWhenNull = "Invalid object")
        {
            validationResults = new List<ValidationResult>();

            if (item == null)
            {
                validationResults.Add(new ValidationResult(messageWhenNull));
                return false;
            }
            var context = new ValidationContext(item, serviceProvider: null, items: null);
            return Validator.TryValidateObject(item, context, validationResults, true);
        }

        /// <summary>
        /// Validate Data Annotation Attributes
        /// </summary>
        /// <param name="item"></param>
        /// <param name="messageWhenNull"></param>
        /// <returns>Object with result: Success and messages</returns>
        public Result Validate(object item, string messageWhenNull = "Invalid object")
        {
            var result = new Result
            {
                IsSuccess = ValidateObject(item, out List<ValidationResult> validationResults, messageWhenNull)
            };
            foreach (var validationResult in validationResults)
            {
                result.Messages.Add(validationResult.ErrorMessage ??
                    $"Validation error at '{validationResult.MemberNames?.FirstOrDefault()}'");
            }

            return result;
        }
    }
}
