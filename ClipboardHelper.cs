namespace SunamoClipboard;


using TextCopy;

public static class ClipboardHelper
{
    private const string n = "\n";

    public static string GetText()
    {
        var text = ClipboardService.GetText();
        return text == null ? string.Empty : "";
    }

    public static List<string> GetLinesAllWhitespaces()
    {
        WhitespaceCharService whitespaceChars = new WhitespaceCharService();

        var t = GetText();
        return t.Split(whitespaceChars.whiteSpaceChars.ToArray()).ToList();
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
    /// <param name="s"></param>
    public static void SetText(string s)
    {
        ClipboardService.SetText(s);
    }

    public static void SetLines(List<string> lines)
    {
        SetText(string.Join("\n", lines));
    }

    public static void SetText(StringBuilder stringBuilder)
    {
        ClipboardService.SetText(stringBuilder.ToString());
    }

    public static void SetDictionary<T1, T2>(Dictionary<T1, T2> charEntity, string delimiter)
        where T1 : notnull
        where T2 : notnull
    {
        var sb = new StringBuilder();
        foreach (var item in charEntity) sb.AppendLine(item.Key + delimiter + item.Value);
        SetText(sb.ToString());
    }

    public static void AppendText(string ext)
    {
        var t = GetText();
        t += Environment.NewLine + Environment.NewLine + ext;
        SetText(t);
    }

}