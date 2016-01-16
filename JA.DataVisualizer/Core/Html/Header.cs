﻿namespace JA.DataVisualizer.Core.Html
{
    public static class Header
    {
        public static string Render()
        {
            var headerDiv = new TagBuilder("header");

            var metaTag1 = new TagBuilder("meta");
            metaTag1.MergeAttribute("http-equiv", "Content-Type");
            metaTag1.MergeAttribute("content", "text/html;charset=utf-8");

            var metaTag2 = new TagBuilder("meta");
            metaTag2.MergeAttribute("http-equiv", "X-UA-Compatible");
            metaTag2.MergeAttribute("content", "IE=edge");

            var metaTag3 = new TagBuilder("meta");
            metaTag3.MergeAttribute("http-equiv", "Content-Type");
            metaTag3.MergeAttribute("content", "text/html;charset=utf-8");

            var cssStyleTag = new TagBuilder("style");
            cssStyleTag.MergeAttribute("type", "text/css");
            cssStyleTag.InnerHtml += Css;

            headerDiv.InnerHtml += metaTag1.ToString();
            headerDiv.InnerHtml += metaTag2.ToString();
            headerDiv.InnerHtml += metaTag3.ToString();
            headerDiv.InnerHtml += cssStyleTag.ToString();

            return headerDiv.ToString();
        }

        private static string Css { get; } = "body { margin: 0.3em 0.3em 0.4em 0.4em; font-family: Verdana; font-size: 80%; background: white; }" +
                                             "p, pre { margin: 0; padding: 0; font-family: Verdana;}" +
                                             "table { border-collapse: collapse; border: 2px solid #17b; margin: 0.3em 0.2em; } " +
                                             "table.limit {border-collapse: collapse; border-bottom: 2px solid #c31; } " +
                                             "td, th { vertical-align: top; border: 1px solid #aaa; padding: 0.1em 0.2em; margin: 0; }" +
                                             "th { text-align: left; background-color: #ddd; border: 1px solid #777; font-family: tahoma; font-size:90%; font-weight: bold; }" +
                                             "th.member { padding: 0.1em 0.2em 0.1em 0.2em;}" +
                                             "td.typeheader { font-family: tahoma;font-size: 100%;font-weight: bold;background-color: #17b;color: white;padding: 0 0.2em 0.15em 0.1em; }" +
                                             "td.n { text-align: right }" +
                                             "a: link.typeheader, a: visited.typeheader, a: link.extenser, a: visited.extenser { font-family: tahoma; font-size: 90 %; font-weight: bold; text-decoration: none; background-color: #17b; color: white; float:left; }" +
                                             "a: link.extenser, a: visited.extenser { float:right; padding-left:2pt; margin-left:4pt } " +
                                             "span.typeglyph, span.typeglyphx { padding: 0 0.2em 0 0; margin: 0; }span.extenser, span.extenserx { margin-top:1.2pt;}" +
                                             "span.typeglyph, span.extenser { font-family: webdings;}" +
                                             "span.typeglyphx, span.extenserx { font-family: arial; font-weight: bold; margin: 2px;}" +
                                             "table.group { border: none; margin: 0;}" +
                                             "td.group { border: none; padding: 0 0.1em;}" +
                                             "div.spacer { margin: 0.6em 0;}" +
                                             "table.headingpresenter {border: none; border-left: 3px dotted #1a5;	margin: 1em 0em 1.2em 0.15em;}" +
                                             "th.headingpresenter { font-family: Arial; border: none; padding: 0 0 0.2em 0.5em; background-color: white; color: green; font-size: 110 %;}" +
                                             "td.headingpresenter { border: none; padding: 0 0 0 0.6em;}" +
                                             "td.summary {background-color: #def;	color: #024;	font-family: Tahoma;	padding: 0 0.1em 0.1em 0.1em;}" +
                                             "td.columntotal {font-family: Tahoma; background-color: #eee;	font-weight: bold;	color: #17b;	font-size:90%;	text-align:right;}" +
                                             "span.graphbar {background: #17b;	color: #17b;	margin-left: -2px;	margin-right: -2px;}" +
                                             "a: link.graphcolumn, a: visited.graphcolumn {color: #17b;	text-decoration: none;	font-weight: bold;	font-family: Arial;	font-size: 110%;	letter-spacing: -0.2em;		margin-left: 0.3em;	margin-right: 0.1em;}" +
                                             "a: link.collection, a: visited.collection { color: green }a: link.reference, a: visited.reference { color: blue }" +
                                             "i { color: green;}" +
                                             "em { color: red;}" +
                                             "span.highlight {background: #ff8; }" +
                                             "code { font-family: Consolas }code.xml b { color: blue; font-weight:normal }" +
                                             "code.xml i { color: maroon; font-weight:normal; font-style:normal }code.xml em { color: red; font-weight:normal; font-style:normal }";
    }
}
