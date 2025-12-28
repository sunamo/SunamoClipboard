// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoClipboard._sunamo.SunamoStringGetLines;

internal class SHGetLines
{
    internal static List<string> GetLines(string input)
    {
        var parts = input.Split(new string[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    private static void SplitByUnixNewline(List<string> lines)
    {
        SplitBy(lines, "\r");
        SplitBy(lines, "\n");
    }

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