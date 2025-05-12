using HtmlAgilityPack;

namespace TheBoys.API.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string value) =>
        value is not null && !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value);

    public static string StripHtml(string html)
    {
        if (string.IsNullOrEmpty(html))
            return string.Empty;

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // نجمع كل النصوص داخل الـ nodes
        var text = doc.DocumentNode.InnerText.Trim();

        return text.HasValue() ? text : html;
    }
}
