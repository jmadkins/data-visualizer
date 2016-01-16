using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JA.DataVisualizer.Core.Csv
{
    internal class CsvHelper
    {
        public static string ToCommaDelimitedList<T>(IList<T> listOfItems, string delim = ",")
        {
            var sb = new StringBuilder();

            if (listOfItems.Any())
            {
                foreach (var property in listOfItems.First().GetType().GetProperties())
                {
                    //TODO: check to see if the property has a display attribute
                    sb.Append($"{property.Name}{delim}");
                }

                sb.Append(Environment.NewLine);
            }

            foreach (var item in listOfItems)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    var value = property.GetValue(item) ?? string.Empty;

                    if (property.PropertyType == typeof(bool))
                    {
                        var valueAsBool = (bool)value;
                        value = valueAsBool
                            ? "Yes"
                            : "No";
                    }
                    else
                    {
                        var valueAsEnum = value as Enum;
                        var valueAsString = value.ToString();

                        if (valueAsEnum != null)
                        {
                            value = $"\"{valueAsEnum.GetDescription()}\"";
                        }
                        else if (valueAsString.Contains(delim))
                        {
                            if (valueAsString.Contains("\""))
                            {
                                value = valueAsString.Replace("\"", "\"\"");
                            }
                            value = $"\"{value}\"";
                        }
                        else if (Regex.IsMatch(valueAsString, @"^[\s]*[0-9]+[\s]*$"))
                        {
                            value = $"=\"{value}\"";
                        }
                    }

                    sb.Append($"{value}{delim}");
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        //public static string ExportCsv(object[] values, string delim = ",")
        //{
        //    return ExportCsv(values.Select(r => r.ToString()).ToArray(), delim);
        //}

        //public static string ExportCsv(string[] values, string delim = ",")
        //{
        //    var csvValues = new string[values.Length];

        //    for (var i = 0; i < values.Length; i++)
        //    {
        //        var csvValue = values[i];
        //        if (csvValue.Contains(delim))
        //        {
        //            if (csvValue.Contains("\""))
        //            {
        //                csvValue = csvValue.Replace("\"", "\"\"");
        //            }
        //            csvValue = $"\"{csvValue}\"";
        //        }
        //        else if (Regex.IsMatch(csvValue, @"^[\s]*[0-9]+[\s]*$"))    //if contains only digits, wrap it in ="" so that Excel won't remove any leading zeros
        //        {
        //            csvValue = $"=\"{csvValue}\"";
        //        }
        //        csvValues[i] = csvValue;
        //    }

        //    var returnValue = string.Join(delim, csvValues);
        //    return returnValue;
        //}

        //private static string[] GetValuesFromCSV(string row)
        //{
        //    var returnValue = new List<string>();
        //    var isInsideDoubleQuotes = false;
        //    var sb = new StringBuilder();
        //    var pos = 0;

        //    while (pos < row.Length)
        //    {
        //        var c = row[pos];

        //        if (c == '"')
        //        {
        //            if (pos + 1 < row.Length && row[pos + 1] == '"')
        //            {
        //                sb.Append('"');
        //                pos++;
        //            }
        //            else
        //            {
        //                isInsideDoubleQuotes = !isInsideDoubleQuotes;
        //            }
        //        }
        //        else if (c == ',' && !isInsideDoubleQuotes)
        //        {
        //            returnValue.Add(sb.ToString());
        //            sb = new StringBuilder();
        //        }
        //        else
        //        {
        //            sb.Append(c);
        //        }

        //        pos++;
        //    }
        //    returnValue.Add(sb.ToString());

        //    return returnValue.ToArray();
        //}
    }
}
