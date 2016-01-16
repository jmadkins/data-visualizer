using System.Collections.Generic;
using System.IO;
using System.Linq;
using JA.DataVisualizer.Core.Html;

namespace JA.DataVisualizer.Core
{
    public static partial class Export
    {
        public static void ExportHtml(this object data, string filePath, string reportName, bool appendToExistingFile = false)
        {
            using (var sw = new StreamWriter(filePath, appendToExistingFile))
            {
                var page = new TagBuilder("html");

                page.InnerHtml += Header.Render();

                var bodyTag = new TagBuilder("body");
                bodyTag.InnerHtml += $"{data}";

                page.InnerHtml += bodyTag.ToString();

                sw.Write(page.ToString());
                sw.Close();
            }
        }

        public static void ExportHtml<T, TW>(this Dictionary<T, TW> data, string filePath, string reportName, bool appendToExistingFile = false)
        {
            var dude = data.Select(p => new {p.Key, p.Value}).ToList();
            ExportHtml(dude, filePath, reportName, appendToExistingFile);
        }

        public static void ExportHtml<T>(this IEnumerable<T> data, string filePath, string reportName, bool appendToExistingFile = false)
        {
            using (var sw = new StreamWriter(filePath, appendToExistingFile))
            {
                var page = new TagBuilder("html");

                page.InnerHtml += Header.Render();

                var bodyTag = new TagBuilder("body");

                bodyTag.InnerHtml += TableBuilder.Build(data, reportName);

                page.InnerHtml += bodyTag.ToString();

                sw.Write(page.ToString());
                sw.Close();
            }
        }
    }
}
