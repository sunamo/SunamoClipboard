namespace SunamoClipboard._sunamo.SunamoStringGetLines;

internal class SHGetLines
{
    internal static List<string> GetLines(string text)
    {
        var lines = text.Split(new string[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(lines);
        return lines;
    }

    private static void SplitByUnixNewline(List<string> list)
    {
        SplitBy(list, "\r");
        SplitBy(list, "\n");
    }

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