using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Zanella.DocumentHelper.Exceptions;
using Zanella.DocumentHelper.Util;

namespace Zanella.DocumentHelper.CSV
{
    /// <summary>
    /// CSV Helper
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Bind CSV content to objects
        /// </summary>
        /// <param name="content">CSV content</param>
        /// <param name="cultureInfo"></param>
        /// <param name="separators">List of separator for columns, default: ';'</param>
        /// <param name="removeHeaderWithError">Remove first line if contains error</param>
        /// <returns>List of objects</returns>
        public static IList<T> Read<T>(string content
            , IFormatProvider cultureInfo = null
            , string[] separators = null
            , bool removeHeaderWithError = false
            ) where T : ICSVObject, new()
        {
            return Read<T>(content, null, cultureInfo, separators, removeHeaderWithError);
        }

        /// <summary>
        /// Write CSV content
        /// </summary>
        /// <typeparam name="T">Object, if has <see cref="CSV_ColumnAttribute"/> Properties read only that values, else read all public properties</typeparam>
        /// <param name="data"></param>
        /// <param name="separator">default: ';'</param>
        /// <param name="filePath">Write file on disk</param>
        /// <returns>The conten of file</returns>
        public static string Write<T>(IEnumerable<T> data
          , string separator = ";"
          , string filePath = null
        )
        {
            return Write(data, null, separator, filePath);
        }

        internal static IList<T> Read<T>(string content
            , IEnumerable<MappedProperty> writeProperties
            , IFormatProvider cultureInfo = null
            , string[] separators = null
            , bool removeHeaderWithError = false
            ) where T : ICSVObject, new()
        {
            if (writeProperties == null || !writeProperties.Any())
            {
                var type = new T().GetType();
                writeProperties = GetProperties(type, true);
            }

            var list = new List<T>();
            if (string.IsNullOrWhiteSpace(content))
                return list;

            separators = separators ?? new string[] { ";" };

            using (var sr = new StringReader(content))
            {
                string line;
                T dataObject;
                var lineNumber = 0;
                string[] data;
                var column = 0;
                object convertedValue;
                MappedProperty prop;

                while ((line = sr.ReadLine()) != null)
                {
                    lineNumber++;
                    dataObject = new T()
                    {
                        Exceptions = new List<Exception>(),
                        Line = lineNumber,
                    };

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        dataObject.Exceptions.Add(new EmptyLineException());
                        list.Add(dataObject);
                        continue;
                    }

                    data = line.Split(separators, StringSplitOptions.None);
                    column = 0;
                    foreach (var valor in data)
                    {
                        prop = writeProperties.FirstOrDefault(x => x.Position == column);
                        column++;

                        if (prop == null)
                            continue;

                        try
                        {
                            if (string.IsNullOrWhiteSpace(valor))
                            {
                                if (prop.IsRequired)
                                {
                                    dataObject.Exceptions.Add(new RequiredException(column
                                        , prop.Example ?? prop.Property.GetValue(dataObject)?.ToString()));
                                    continue;
                                }

                                if (prop.Property.PropertyType == typeof(string))
                                    convertedValue = valor;
                                else
                                    continue;
                            }
                            else
                            {
                                if (prop.Property.PropertyType == typeof(string))
                                    convertedValue = valor;
                                else if (prop.Property.PropertyType == typeof(bool))
                                    convertedValue = TransformBoolValue(valor);
                                else if (Nullable.GetUnderlyingType(prop.Property.PropertyType) != null)
                                {
                                    var tipoInterno = Nullable.GetUnderlyingType(prop.Property.PropertyType);
                                    if (tipoInterno == typeof(bool))
                                        convertedValue = TransformBoolValue(valor);
                                    else
                                        convertedValue = Convert.ChangeType(valor, tipoInterno, cultureInfo);
                                }
                                else
                                    convertedValue = Convert.ChangeType(valor, prop.Property.PropertyType, cultureInfo);

                            }

                            prop.Property.SetValue(dataObject, convertedValue);
                        }
                        catch (Exception ex)
                        {
                            dataObject.Exceptions.Add(
                                new InvalidValueException(valor, column, prop.Example ?? prop.Property.GetValue(dataObject)?.ToString(), ex));
                        }
                    }

                    list.Add(dataObject);
                }
            }

            if (removeHeaderWithError)
            {
                var header = list.FirstOrDefault(x => x.Line == 1);
                if (header != null && header.Exceptions.Any())
                    list.RemoveAt(0);
            }

            return list;
        }

        internal static string Write<T>(IEnumerable<T> data
          , IEnumerable<MappedProperty> readProperties
          , string separator = ";"
          , string filePath = null
        )
        {
            if (data == null || !data.Any())
            {
                return null;
            }

            if (readProperties == null || !readProperties.Any())
            {
                var type = data.First().GetType();
                readProperties = GetProperties(type, false).OrderBy(x => x.Position);
            }
            var sb = new StringBuilder();
            string line;

            var writeFile = !string.IsNullOrEmpty(filePath);
            StreamWriter sw = null;
            if (writeFile)
                sw = new StreamWriter(filePath);

            foreach (var item in data)
            {
                line = GetLine(separator, readProperties.Select(x => x.Property.GetValue(item)).ToArray());
                sb.AppendLine(line);
                if (writeFile)
                    sw.WriteLine(line);
            }
            sw?.Dispose();

            return sb.ToString();
        }

        private static object TransformBoolValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidCastException();

            var stringBool = value.Trim().ToLower();
            switch (stringBool)
            {
                case "true":
                case "yes":
                case "sim":
                case "y":
                case "1":
                case "s":
                case "ativo":
                case "active":
                case "apto":
                case "apt":
                    return true;
                case "false":
                case "no":
                case "não":
                case "nao":
                case "n":
                case "0":
                case "inativo":
                case "inactive":
                case "inapto":
                case "unqualified":
                    return false;
                default:
                    return stringBool;
            }
        }

        private static string GetLine(string separator, params object[] data)
        {
            if (data.Length > 0 && data[0] == null)
            {
                data[0] = string.Empty;
            }
            return string.Join(separator, data);
        }

        internal static List<MappedProperty> GetProperties(Type type, bool write)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (write)
                properties = properties.Where(p => p.CanWrite).ToArray();
            else
                properties = properties.Where(p => p.CanRead).ToArray();

            if (!properties.Any())
                throw new InvalidOperationException("Object without compative Properties");

            var mappedProperties = new List<MappedProperty>();
            MappedProperty prop;
            foreach (var property in properties)
            {
                var atributo = property.GetCustomAttribute<CSV_ColumnAttribute>(false);
                if (atributo == null)
                    continue;

                prop = new MappedProperty()
                {
                    Property = property,
                    Position = atributo.Position,
                    IsRequired = atributo.IsRequired,
                    Example = atributo.Example,
                };

                mappedProperties.Add(prop);
            }

            if (!mappedProperties.Any())
            {
                if (write)
                    throw new InvalidOperationException("Object with none CSV_ColumnAttribute Property");
                else
                {
                    int position = 0;
                    foreach (var property in properties)
                    {
                        prop = new MappedProperty()
                        {
                            Property = property,
                            Position = position,
                        };
                        mappedProperties.Add(prop);
                        position++;
                    }
                }
            }

            if (mappedProperties.GroupBy(x => x.Position).Any(g => g.Count() > 1))
                throw new InvalidOperationException("Duplicate position on CSV_ColumnAttribute");

            return mappedProperties;
        }
    }

    /// <summary>
    /// CSV Helper
    /// </summary>
    /// <typeparam name="T">Data Type</typeparam>
    public class Helper<T> : Helper where T : ICSVObject, new()
    {
        private IEnumerable<MappedProperty> _writeProperties = new List<MappedProperty>();
        private IEnumerable<MappedProperty> _readProperties = new List<MappedProperty>();

        /// <summary>
        /// Bind CSV content to objects
        /// </summary>
        /// <param name="content">CSV content</param>
        /// <param name="cultureInfo"></param>
        /// <param name="separators">List of separator for columns, default: ';'</param>
        /// <param name="removeHeaderWithError">Remove first line if contains error</param>
        /// <returns>List of objects</returns>
        public IList<T> Read(string content
           , IFormatProvider cultureInfo = null
           , string[] separators = null
           , bool removeHeaderWithError = false
           )
        {
            if (_writeProperties == null || !_writeProperties.Any())
            {
                var type = new T().GetType();
                _writeProperties = GetProperties(type, true);
            }

            return Read<T>(content, _writeProperties, cultureInfo, separators, removeHeaderWithError);
        }

        /// <summary>
        /// Write CSV content
        /// </summary>        
        /// <param name="data"></param>
        /// <param name="separator">default: ';'</param>
        /// <param name="filePath">Write file on disk</param>
        /// <returns>The conten of file</returns>
        public string Write(IEnumerable<T> data
            , string separator = ";"
            , string filePath = null
        )
        {
            if (_readProperties == null || !_readProperties.Any())
            {
                var type = new T().GetType();
                _readProperties = GetProperties(type, false).OrderBy(x => x.Position);
            }
            return Write(data, _readProperties, separator, filePath);
        }
    }
}
