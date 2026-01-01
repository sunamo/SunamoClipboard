namespace SunamoClipboard._sunamo.SunamoStringGetLines;

/// <summary>
/// Provides methods for splitting text into lines with proper handling of different newline formats.
/// </summary>
internal class SHGetLines
{
    /// <summary>
    /// Splits the input text into lines, handling various newline formats (CRLF, LFCR, CR, LF).
    /// </summary>
    /// <param name="input">The input text to split into lines.</param>
    /// <returns>A list of lines from the input text.</returns>
    internal static List<string> GetLines(string input)
    {
        var parts = input.Split(new string[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    /// <summary>
    /// Splits lines by Unix-style newline characters (CR and LF).
    /// </summary>
    /// <param name="lines">The list of lines to split.</param>
    private static void SplitByUnixNewline(List<string> lines)
    {
        SplitBy(lines, "\r");
        SplitBy(lines, "\n");
    }

    /// <summary>
    /// Splits lines by the specified delimiter, ensuring no CRLF or LFCR patterns remain.
    /// </summary>
    /// <param name="lines">The list of lines to split.</param>
    /// <param name="delimiter">The delimiter to split by.</param>
    /// <exception cref="Exception">Thrown if the lines contain CRLF or LFCR patterns when splitting by CR.</exception>
    private static void SplitBy(List<string> lines, string delimiter)
    {
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var carriageReturnNewlineParts = lines[i].Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var newlineCarriageReturnParts = lines[i].Split(new string[] { "\n\r" }, StringSplitOptions.None);

                if (carriageReturnNewlineParts.Length > 1)
                {
                    throw new Exception("cannot contain any \r\name, pass already split by this pattern");
                }
                else if (newlineCarriageReturnParts.Length > 1)
                {
                    throw new Exception("cannot contain any \n\r, pass already split by this pattern");
                }
            }

            var splitParts = lines[i].Split(new string[] { delimiter }, StringSplitOptions.None);

            if (splitParts.Length > 1)
            {
                InsertOnIndex(lines, splitParts.ToList(), i);
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
