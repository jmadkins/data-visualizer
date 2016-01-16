using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace JA.DataVisualizer.Core.Html
{
    public static class TableBuilder
    {
        public static string Build<T>(IEnumerable<T> collection, string reportName, Func<T, object> propertySelector = null)
        {
            var returnValue = new TagBuilder("table");

            var properies = new List<PropertyInfo>();

            var header = new TagBuilder("thead");
            var headerRow = new TagBuilder("tr");

            var properties = collection.First().GetType().GetProperties();

            headerRow.InnerHtml += $@"<tr><td class='typeheader' colspan='{properties.Length}'>{reportName}</td></tr>";

            foreach (var property in properties)
            {
                var realProperty = properties.FirstOrDefault(m => m.Name == property.Name) ?? property;
                properies.Add(realProperty); //to minimize the number of times we must use reflection, we 'cache' the PropertyInfo in the TableDefinition
                headerRow.InnerHtml += CreateColumnHeaders<T>(realProperty);
            }

            header.InnerHtml += string.Format("{0}\t\t{1}", Environment.NewLine, headerRow);
            returnValue.InnerHtml += string.Format("{0}\t{1}", Environment.NewLine, header);

            var body = new TagBuilder("tbody");

            foreach (var item in collection)
            {
                var bodyRow = new TagBuilder("tr");

                foreach (var property in properies)
                {
                    string cell = GetCellValue<T>(property, item);

                    bodyRow.InnerHtml += string.Format("{0}\t\t{1}", Environment.NewLine, cell);
                }

                body.InnerHtml += string.Format("{0}\t{1}", Environment.NewLine, bodyRow);
            }

            var footer = new TagBuilder("footer");

            returnValue.InnerHtml += string.Format("{0}\t{1}", Environment.NewLine, body);

            return returnValue.ToString();
        }

        /// <summary>
        /// Creates the headers for every column. If a specific column is sortable, the links will automatically be created
        /// </summary>
        /// /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string CreateColumnHeaders<T>(PropertyInfo property)
        {
            var returnValue = new TagBuilder("th");

            var displayAttribute = property.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
            var displayName = displayAttribute != null
                ? displayAttribute.Description
                : property.Name.Replace("_", " ");

            returnValue.InnerHtml = displayName;

            returnValue.MergeAttribute("class", property.Name);

            if (IsNumeric(property.PropertyType))
            {
                returnValue.MergeAttributes(GetNumericFormatting());
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// Given a variety of different factors, gets the value to display for a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="property"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static string GetCellValue<T>(PropertyInfo property, object item)
        {
            var cell = new TagBuilder("td");
            cell.MergeAttribute("class", property.Name);

            if (IsNumeric(property.PropertyType))
            {
                cell.MergeAttributes(GetNumericFormatting());
            }

            var value = GetPropertyValue(property, item);
            var valueAsEnum = value as Enum;

            var cellValue = string.Empty;
            if (valueAsEnum != null)
            {
                cellValue += valueAsEnum.GetDescription();
            }
            else if (property.PropertyType == typeof(bool))
            {
                var valueAsBool = (bool)value;
                cellValue = valueAsBool
                    ? "Yes"
                    : "No";
            }
            //else if (HasUiHintDefined(property) && value != null)
            //{
            //    var attribute = (UIHintAttribute)property.GetCustomAttributes(typeof(UIHintAttribute), true).Single();
            //    cellValue += helper.DisplayFor(p => value, attribute.UIHint);
            //}
            //else if (HasDisplayFormatDefined(property))
            //{
            //    var attribute = (DisplayFormatAttribute)property.GetCustomAttributes(typeof(DisplayFormatAttribute), true).Single();
            //    cellValue = attribute.NullDisplayText;
            //}
            else if (value == null)
            {
                cellValue = string.Empty;
            }
            else
            {
                cellValue = value.ToString();
            }

            cell.InnerHtml += cellValue;
            return cell.ToString();
        }

        #region Property Helpers

        private static object GetPropertyValue(PropertyInfo property, object item)
        {
            var itemType = item.GetType();
            var origionalProp = itemType.GetProperty(property.Name);
            return origionalProp.GetValue(item);
        }

        public static bool IsNumeric(Type type)
        {
            return false;
            //return (Nullable.GetUnderlyingType(type) ?? type).IsIn(
            //    typeof(int),
            //    typeof(double),
            //    typeof(decimal),
            //    typeof(long),
            //    typeof(short),
            //    typeof(sbyte),
            //    typeof(byte),
            //    typeof(ulong),
            //    typeof(ushort),
            //    typeof(uint),
            //    typeof(float)
            //    );
        }

        public static Dictionary<string, string> GetNumericFormatting()
        {
            return new Dictionary<string, string> { { "style", "text-align: right" } };
        }

        #endregion
    }
}
