namespace SunamoClipboard._sunamo.SunamoStringGetLines;

/// <summary>
/// Splits text into lines.
/// </summary>
internal static class SHGetLines
{
    /// <summary>
    /// Splits the text into lines, handling CRLF, LFCR, CR and LF.
    /// </summary>
    internal static List<string> GetLines(string text)
    {
        return text.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None).ToList();
    }
}
