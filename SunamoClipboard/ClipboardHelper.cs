namespace SunamoClipboard;

public static class ClipboardHelper
{
    private const string newLine = "\n";
    public static string GetText()
    {
        var text = ClipboardService.GetText();
        return text == null ? string.Empty : "";
    }
    public static List<string> GetLinesAllWhitespaces()
    {
        WhitespaceCharService whitespaceChars = new WhitespaceCharService();
        var text = GetText();
        return text.Split(whitespaceChars.WhiteSpaceChars.ToArray()).ToList();
    }
    public static List<string> GetLines()
    {
        var text = ClipboardService.GetText();
        if (text == null) return new List<string>();
        return SHGetLines.GetLines(text);
    }
    /// <summary>
    ///     Cant be se or only whitespace => even with ClipboardHelper.SetText(v); => content of clipboard will remain the same
    ///     Must
    /// </summary>
    /// <param name="text"></param>
    public static void SetText(string text)
    {
        ClipboardService.SetText(text);
    }
    public static void SetLines(List<string> lines)
    {
        SetText(string.Join("\n", lines));
    }
    public static void SetText(StringBuilder stringBuilder)
    {
        ClipboardService.SetText(stringBuilder.ToString());
    }
    public static void SetDictionary<T1, T2>(Dictionary<T1, T2> dictionary, string delimiter)
        where T1 : notnull
        where T2 : notnull
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in dictionary) stringBuilder.AppendLine(item.Key + delimiter + item.Value);
        SetText(stringBuilder.ToString());
    }
    public static void AppendText(string textToAppend)
    {
        var currentText = GetText();
        currentText += Environment.NewLine + Environment.NewLine + textToAppend;
        SetText(currentText);
    }
}