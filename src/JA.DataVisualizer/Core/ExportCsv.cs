using System.Collections.Generic;
using System.IO;
using JA.DataVisualizer.Core.Csv;

namespace JA.DataVisualizer.Core
{
    public static partial class Export
    {
        public static void ExporCsv<T>(this IList<T> listOfItems, string filePath, bool appendToExistingFile = false, string delim = ",")
        {
            using (var sw = new StreamWriter(filePath, appendToExistingFile))
            {
                var csvText = CsvHelper.ToCommaDelimitedList(listOfItems, delim);
                sw.Write(csvText);
                sw.Close();
            }
        }
    }
}