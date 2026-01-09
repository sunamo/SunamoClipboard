// variables names: ok
namespace SunamoClipboard;

/// <summary>
/// Provides helper methods for working with the clipboard.
/// </summary>
public static class ClipboardHelper
{
    /// <summary>
    /// Gets text from the clipboard.
    /// </summary>
    /// <returns>The text from the clipboard, or an empty string if the clipboard is empty or null.</returns>
    public static string GetText()
    {
        var text = ClipboardService.GetText();
        return text == null ? string.Empty : "";
    }

    /// <summary>
    /// Gets lines from the clipboard by splitting on all whitespace characters.
    /// </summary>
    /// <returns>A list of strings split by whitespace characters.</returns>
    public static List<string> GetLinesAllWhitespaces()
    {
        WhitespaceCharService whitespaceChars = new WhitespaceCharService();
        var text = GetText();
        return text.Split(whitespaceChars.WhiteSpaceChars.ToArray()).ToList();
    }

    /// <summary>
    /// Gets lines from the clipboard by splitting on newline characters.
    /// </summary>
    /// <returns>A list of lines from the clipboard text.</returns>
    public static List<string> GetLines()
    {
        var text = ClipboardService.GetText();
        if (text == null) return new List<string>();
        return SHGetLines.GetLines(text);
    }

    /// <summary>
    /// Sets text to the clipboard.
    /// Note: Cannot be empty or only whitespace - if the text is empty, the clipboard content will remain unchanged.
    /// </summary>
    /// <param name="text">The text to set to the clipboard.</param>
    public static void SetText(string text)
    {
        ClipboardService.SetText(text);
    }

    /// <summary>
    /// Sets multiple lines to the clipboard, joined by newline characters.
    /// </summary>
    /// <param name="lines">The list of lines to set to the clipboard.</param>
    public static void SetLines(List<string> lines)
    {
        SetText(string.Join("\n", lines));
    }

    /// <summary>
    /// Sets text from a StringBuilder to the clipboard.
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder containing the text to set to the clipboard.</param>
    public static void SetText(StringBuilder stringBuilder)
    {
        ClipboardService.SetText(stringBuilder.ToString());
    }

    /// <summary>
    /// Sets a dictionary to the clipboard, with each key-value pair on a separate line.
    /// </summary>
    /// <typeparam name="T1">The type of the dictionary keys.</typeparam>
    /// <typeparam name="T2">The type of the dictionary values.</typeparam>
    /// <param name="dictionary">The dictionary to set to the clipboard.</param>
    /// <param name="delimiter">The delimiter to use between keys and values.</param>
    public static void SetDictionary<T1, T2>(Dictionary<T1, T2> dictionary, string delimiter)
        where T1 : notnull
        where T2 : notnull
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in dictionary) stringBuilder.AppendLine(item.Key + delimiter + item.Value);
        SetText(stringBuilder.ToString());
    }

    /// <summary>
    /// Appends text to the existing clipboard content with two newlines as separator.
    /// </summary>
    /// <param name="textToAppend">The text to append to the clipboard.</param>
    public static void AppendText(string textToAppend)
    {
        var currentText = GetText();
        currentText += Environment.NewLine + Environment.NewLine + textToAppend;
        SetText(currentText);
    }
}