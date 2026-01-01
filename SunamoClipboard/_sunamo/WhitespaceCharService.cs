namespace SunamoClipboard._sunamo;

/// <summary>
/// Provides a service for working with whitespace characters.
/// </summary>
internal class WhitespaceCharService
{
    /// <summary>
    /// Gets the list of whitespace characters.
    /// </summary>
    internal List<char> WhiteSpaceChars { get; }

    /// <summary>
    /// Gets the list of whitespace character codes.
    /// </summary>
    internal readonly List<int> WhiteSpacesCodes = new(new[]
    {
        9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202,
        8232, 8233, 8239, 8287, 12288
    });

    /// <summary>
    /// Initializes a new instance of the <see cref="WhitespaceCharService"/> class.
    /// </summary>
    internal WhitespaceCharService()
    {
        WhiteSpaceChars = WhiteSpacesCodes.Select(code => (char)code).ToList();
    }
}
