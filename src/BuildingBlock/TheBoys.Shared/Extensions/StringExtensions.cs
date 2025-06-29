using System.Text.RegularExpressions;
using TheBoys.Shared.Enums;
using TheBoys.Shared.Misc;

namespace TheBoys.Shared.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string value) =>
        value is not null && !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value);

    public static string GetFullPath(Guid ownerId, string imgName)
    {
        if (!imgName.HasValue())
            return string.Empty;
        if (ImageHelper.images.TryGetValue(ownerId.ToString().ToLower(), out string path))
        {
            return $"{path}{imgName}";
        }
        else if (Directory.Exists($"wwwroot/uploads/{imgName}"))
        {
            return $"https://stage.menofia.edu.eg/uploads/{imgName}";
        }
        else
        {
            return $"http://mu.menofia.edu.eg/PrtlFiles/Sectors/Wafiden/Portal/Images/{imgName}";
        }
    }

    public static string StripHtml(string html)
    {
        if (!html.HasValue())
            return string.Empty;

        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.OptionFixNestedTags = true;
        doc.LoadHtml(html);

        var text = doc.DocumentNode.InnerText;

        text = System.Net.WebUtility.HtmlDecode(text);
        text = text.Replace("\r", " ").Replace("\n", " ");
        text = Regex.Replace(text, @"\s+", " ");
        text = text.Trim();
        return text.HasValue() ? text : string.Empty;
    }

    public static bool IsValidPassword(this string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;
        // at least: 1 Capital char, 1 sympol , min length is 8. P@sswrod
        string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[A-Za-z\d\W_]{8,}$";
        return Regex.IsMatch(password, pattern);
    }

    public static bool IsImage(this string fileName)
    {
        var extension = Path.GetExtension(fileName)?.TrimStart('.').ToLower();
        return Enum.TryParse(typeof(ImageExtension), extension, true, out _);
    }
}
