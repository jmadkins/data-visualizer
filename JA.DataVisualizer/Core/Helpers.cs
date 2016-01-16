using System;
using System.ComponentModel;

namespace JA.DataVisualizer.Core
{
    public static class Helpers
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null) return string.Empty;
            var returnValue = string.Empty;

            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                if (fieldInfo != null)
                {
                    var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    returnValue = (attributes.Length > 0) ? attributes[0].Description : value.ToString();
                }
                else        //must be a flags enum with multiple values
                {
                    var type = value.GetType();
                    foreach (Enum item in Enum.GetValues(type))
                    {
                        if (((int)Enum.ToObject(type, item) != 0) && (value.HasFlag(item)))     //skip the None value
                        {
                            fieldInfo = type.GetField(item.ToString());
                            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                            if (!string.IsNullOrEmpty(returnValue))
                            {
                                returnValue += ", ";
                            }
                            returnValue += (attributes.Length > 0) ? attributes[0].Description : item.ToString();
                        }
                    }
                }
            }
            catch
            {
                returnValue = value.ToString();
            }

            return returnValue;
        }
    }
}
