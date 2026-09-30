namespace SunamoClipboard._sunamo;

using System.Runtime.InteropServices;


/// <summary>
/// Minimal user32 P/Invoke declarations needed by ClipboardMonitor (copied from SunamoPInvoke to keep this package flat).
/// </summary>
internal static class ClipboardNative
{
    /// <summary>
    /// Message sent when the contents of the clipboard have changed.
    /// </summary>
    internal const int WM_CLIPBOARDUPDATE = 0x031D;

    /// <summary>
    /// Handle value used to create message-only windows.
    /// </summary>
    internal static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

    /// <summary>
    /// Places the given window in the system clipboard format listener list.
    /// </summary>
    /// <param name="hwnd">Handle of the window to receive notifications.</param>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool AddClipboardFormatListener(IntPtr hwnd);

    /// <summary>
    /// Removes the given window from the system clipboard format listener list.
    /// </summary>
    /// <param name="hwnd">Handle of the window to remove.</param>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
}
