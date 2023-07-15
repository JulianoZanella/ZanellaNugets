using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Zanella.CSV.Exceptions;

namespace Zanella.CSV
{
    public class Helper
    {
        /// <summary>
        /// Bind CSV content to objects
        /// </summary>
        /// <typeparam name="T">Data class</typeparam>
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
            var type = new T().GetType();
            var mappedProperties = GetProperties(type, true);

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
                        prop = mappedProperties.FirstOrDefault(x => x.Position == column);
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

        /// <summary>
        /// Write CSV content
        /// </summary>
        /// <typeparam name="T">Object, if has <see cref="CSV_ColumnAttribute"/> Properties read only that values, else all read properties</typeparam>
        /// <param name="data"></param>
        /// <param name="separator">default: ';'</param>
        /// <param name="filePath">Write file in disk</param>
        /// <returns>The conten of file</returns>
        public static string Write<T>(IEnumerable<T> data
            , string separator = ";"
            , string filePath = null
        )
        {
            if (data == null || !data.Any())
            {
                return null;
            }

            var type = data.First().GetType();
            var mappedProperties = GetProperties(type, false).OrderBy(x => x.Position);
            var sb = new StringBuilder();
            string line;

            var writeFile = !string.IsNullOrEmpty(filePath);
            StreamWriter sw = null;
            if (writeFile)
                sw = new StreamWriter(filePath);

            foreach (var item in data)
            {
                line = GetLine(separator, mappedProperties.Select(x => x.Property.GetValue(item)).ToArray());
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

        private static List<MappedProperty> GetProperties(Type type, bool write)
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
}
