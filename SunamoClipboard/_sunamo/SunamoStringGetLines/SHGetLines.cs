namespace SunamoClipboard._sunamo.SunamoStringGetLines;

/// <summary>
/// Provides methods for splitting text into lines with proper handling of different newline formats.
/// </summary>
internal class SHGetLines
{
    /// <summary>
    /// Splits the text into lines, handling various newline formats (CRLF, LFCR, CR, LF).
    /// </summary>
    /// <param name="text">The text to split into lines.</param>
    /// <returns>A list of lines from the text.</returns>
    internal static List<string> GetLines(string text)
    {
        var lines = text.Split(new string[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(lines);
        return lines;
    }

    /// <summary>
    /// Splits lines by Unix-style newline characters (CR and LF).
    /// </summary>
    /// <param name="list">The list to split by Unix newline characters.</param>
    private static void SplitByUnixNewline(List<string> list)
    {
        SplitBy(list, "\r");
        SplitBy(list, "\n");
    }

    /// <summary>
    /// Splits lines by the specified delimiter, ensuring no CRLF or LFCR patterns remain.
    /// </summary>
    /// <param name="list">The list to split by the specified delimiter.</param>
    /// <param name="delimiter">The delimiter to split by.</param>
    /// <exception cref="Exception">Thrown if the list contains CRLF or LFCR patterns when splitting by CR.</exception>
    private static void SplitBy(List<string> list, string delimiter)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var carriageReturnNewlineParts = list[i].Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var newlineCarriageReturnParts = list[i].Split(new string[] { "\n\r" }, StringSplitOptions.None);

                if (carriageReturnNewlineParts.Length > 1)
                {
                    throw new Exception("Text cannot contain \\r\\n at this stage, it should already be split by this pattern");
                }
                else if (newlineCarriageReturnParts.Length > 1)
                {
                    throw new Exception("Text cannot contain \\n\\r at this stage, it should already be split by this pattern");
                }
            }

            var splitParts = list[i].Split(new string[] { delimiter }, StringSplitOptions.None);

            if (splitParts.Length > 1)
            {
                InsertOnIndex(list, splitParts.ToList(), i);
            }
        }
    }

    /// <summary>
    /// Inserts items into a list at the specified index, replacing the item at that index.
    /// </summary>
    /// <param name="list">The list to modify.</param>
    /// <param name="itemsToInsert">The items to insert.</param>
    /// <param name="index">The index at which to insert the items.</param>
    private static void InsertOnIndex(List<string> list, List<string> itemsToInsert, int index)
    {
        itemsToInsert.Reverse();

        list.RemoveAt(index);

        foreach (var item in itemsToInsert)
        {
            list.Insert(index, item);
        }
    }
}