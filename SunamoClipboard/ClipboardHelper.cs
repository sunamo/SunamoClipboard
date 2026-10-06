namespace SunamoClipboard;

public static class ClipboardHelper
{
    public static string GetText()
    {
        var text = ClipboardService.GetText();
        return text ?? string.Empty;
    }

    public static List<string> GetLinesAllWhitespaces()
    {
        WhitespaceCharService whitespaceCharService = new();
        var text = GetText();
        return text.Split(whitespaceCharService.WhiteSpaceChars.ToArray()).ToList();
    }

    public static List<string> GetLines()
    {
        var text = ClipboardService.GetText();
        if (text is null) return [];
        return SHGetLines.GetLines(text);
    }

    public static void SetText(string text)
    {
        ClipboardService.SetText(text);
    }

    public static void SetLines(List<string> list)
    {
        SetText(string.Join("\n", list));
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
