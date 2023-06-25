using System;
using System.ComponentModel;
using System.Linq;

namespace Zanella.Utilities.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get Description from System.ComponentModel.DescriptionAttribute
        /// or Enum.ToString()
        /// </summary>
        /// <param name="enumerator"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? GetDescription(this Enum? enumerator, string? defaultValue = null)
        {
            if (enumerator == null)
                return defaultValue;

            var info = enumerator.GetType().GetField(enumerator.ToString());
            if (info == null)
                return defaultValue;

            var atributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return atributes?.FirstOrDefault()?.Description ?? enumerator.ToString();
        }
    }
}
