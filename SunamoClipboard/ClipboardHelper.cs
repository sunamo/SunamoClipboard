namespace SunamoClipboard;

/// <summary>
/// Represents the clipboard helper type.
/// </summary>
public static class ClipboardHelper
{
    /// <summary>
    /// Get text.
    /// </summary>
    /// <returns>The get text result.</returns>
    public static string GetText()
    {
        var text = ClipboardService.GetText();
        return text ?? string.Empty;
    }

    /// <summary>
    /// Get lines all whitespaces.
    /// </summary>
    /// <returns>The get lines all whitespaces result.</returns>
    public static List<string> GetLinesAllWhitespaces()
    {
        WhitespaceCharService whitespaceCharService = new();
        var text = GetText();
        return text.Split(whitespaceCharService.WhiteSpaceChars.ToArray()).ToList();
    }

    /// <summary>
    /// Get lines.
    /// </summary>
    /// <returns>The get lines result.</returns>
    public static List<string> GetLines()
    {
        var text = ClipboardService.GetText();
        if (text is null) return [];
        return SHGetLines.GetLines(text);
    }

    /// <summary>
    /// Set text.
    /// </summary>
    /// <param name="text">The text.</param>
    public static void SetText(string text)
    {
        ClipboardService.SetText(text);
    }

    /// <summary>
    /// Set lines.
    /// </summary>
    /// <param name="list">The list.</param>
    public static void SetLines(List<string> list)
    {
        SetText(string.Join("\n", list));
    }

    /// <summary>
    /// Set text.
    /// </summary>
    /// <param name="stringBuilder">The string builder.</param>
    public static void SetText(StringBuilder stringBuilder)
    {
        ClipboardService.SetText(stringBuilder.ToString());
    }

    /// <summary>
    /// Set dictionary t 1 t 2.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The set dictionary t 1 t 2 result.</returns>
    public static void SetDictionary<T1, T2>(Dictionary<T1, T2> dictionary, string delimiter)
        where T1 : notnull
        where T2 : notnull
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in dictionary) stringBuilder.AppendLine(item.Key + delimiter + item.Value);
        SetText(stringBuilder.ToString());
    }

    /// <summary>
    /// Append text.
    /// </summary>
    /// <param name="textToAppend">The text to append.</param>
    public static void AppendText(string textToAppend)
    {
        var currentText = GetText();
        currentText += Environment.NewLine + Environment.NewLine + textToAppend;
        SetText(currentText);
    }
}
