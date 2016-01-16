using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace JA.DataVisualizer.Core.Html
{
    public enum TagRenderMode
    {
        Normal,
        StartTag,
        EndTag,
        SelfClosing
    }

    public class TagBuilder
    {
        public string InnerHtml
        {
            get { return _innerHtml ?? string.Empty; }
            set { _innerHtml = value; }
        }

        private string _innerHtml;

        public string TagName { get; private set; }
        public IDictionary<string, string> Attributes { get; }

        public TagBuilder(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("Argument is null or whitespace", nameof(tagName));
            }

            TagName = tagName;
            Attributes = new SortedDictionary<string, string>(StringComparer.Ordinal);
        }

        public void AddCssClass(string value)
        {
            string currentValue;

            if (Attributes.TryGetValue("class", out currentValue))
            {
                Attributes["class"] = value + " " + currentValue;
            }
            else
            {
                Attributes["class"] = value;
            }
        }

        private void AppendAttributes(StringBuilder sb)
        {
            foreach (var attribute in Attributes)
            {
                string key = attribute.Key;
                if (string.Equals(key, "id", StringComparison.Ordinal /* case-sensitive */) && string.IsNullOrEmpty(attribute.Value))
                {
                    continue; // DevDiv Bugs #227595: don't output empty IDs
                }
                string value = attribute.Value; //HttpUtility.HtmlAttributeEncode(attribute.Value);
                sb.Append(' ')
                    .Append(key)
                    .Append("=\"")
                    .Append(value)
                    .Append('"');
            }
        }

        public void MergeAttribute(string key, string value)
        {
            MergeAttribute(key, value, false);
        }

        public void MergeAttribute(string key, string value, bool replaceExisting)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Argument is null or whitespace", nameof(key));
            }

            if (replaceExisting || !Attributes.ContainsKey(key))
            {
                Attributes[key] = value;
            }
        }

        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            MergeAttributes(attributes, false);
        }

        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
        {
            if (attributes != null)
            {
                foreach (var entry in attributes)
                {
                    string key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
                    string value = Convert.ToString(entry.Value, CultureInfo.InvariantCulture);
                    MergeAttribute(key, value, replaceExisting);
                }
            }
        }

        public void SetInnerText(string innerText)
        {
            InnerHtml = innerText; //HttpUtility.HtmlEncode(innderText);
        }

        public override string ToString()
        {
            return ToString(TagRenderMode.Normal);
        }

        public string ToString(TagRenderMode renderMode)
        {
            var sb = new StringBuilder();
            switch (renderMode)
            {
                case TagRenderMode.StartTag:
                    sb.Append('<')
                        .Append(TagName);
                    AppendAttributes(sb);
                    sb.Append('>');
                    break;
                case TagRenderMode.EndTag:
                    sb.Append("</")
                        .Append(TagName)
                        .Append('>');
                    break;
                case TagRenderMode.SelfClosing:
                    sb.Append('<')
                        .Append(TagName);
                    AppendAttributes(sb);
                    sb.Append(" />");
                    break;
                default:
                    sb.Append('<')
                        .Append(TagName);
                    AppendAttributes(sb);
                    sb.Append('>')
                        .Append(InnerHtml)
                        .Append("</")
                        .Append(TagName)
                        .Append('>');
                    break;
            }
            return sb.ToString();
        }
    }
}
