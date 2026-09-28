#if NET9_0_WINDOWS || NET8_0_WINDOWS || NET10_0_WINDOWS || WINDOWS
using System.Windows.Interop;
using SunamoInterfaces.Interfaces;
using SunamoPInvoke.PInvoke;

namespace SunamoClipboard;

/// <summary>
/// Listens to Windows clipboard-update notifications and invokes a callback whenever
/// the clipboard content changes. Only available on Windows (WPF) targets.
/// </summary>
public sealed class ClipboardMonitor : IDisposable, IClipboardMonitor
{
    /// <summary>
    /// Singleton instance of the clipboard monitor.
    /// </summary>
    public static ClipboardMonitor Instance { get; } = new ClipboardMonitor();

    private static bool permanentlyBlockField;
    private static bool? afterSetField = false;

    /// <summary>
    /// Whether after copy to clipboard from any source allow monitoring.
    /// Helper value which changes only IClipboardHelper and IClipboardMonitor:
    /// after a set to clipboard it is set to null, then to true, then finally to false
    /// when everything is ready to use again.
    /// </summary>
    public bool? AfterSet { get => afterSetField; set => afterSetField = value; }

    /// <summary>
    /// Permanently blocks clipboard monitoring.
    /// </summary>
    public bool PermanentlyBlock { get => permanentlyBlockField; set => permanentlyBlockField = value; }

    /// <summary>
    /// Message-only window source used to receive WM_CLIPBOARDUPDATE notifications.
    /// Not available in Mono.
    /// </summary>
    private readonly HwndSource hwndSource = new(0, 0, 0, 0, 0, 0, 0, null, W32.HWND_MESSAGE);

    /// <summary>
    /// Last text content read from the clipboard.
    /// </summary>
    public static string? LastText { get; private set; }

    private Action<string>? actionWithString;

    private ClipboardMonitor()
    {
        try
        {
            hwndSource.AddHook(WndProc);
            W32.AddClipboardFormatListener(hwndSource.Handle);
        }
        catch (Exception)
        {
            // Message-only window could not be created, monitoring stays disabled.
        }
    }

    /// <summary>
    /// Removes the clipboard listener and releases the underlying message-only window.
    /// </summary>
    public void Dispose()
    {
        W32.RemoveClipboardFormatListener(hwndSource.Handle);
        hwndSource.RemoveHook(WndProc);
        hwndSource.Dispose();
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (PermanentlyBlock) return IntPtr.Zero;

        handled = true;

        if (AfterSet is null)
        {
            AfterSet = true;
        }
        else if (AfterSet == true)
        {
            AfterSet = false;
        }
        else if (msg == W32.WM_CLIPBOARDUPDATE)
        {
            if (LastText is null)
            {
                RaiseClipboardChanged();
            }
            else
            {
                string content = ClipboardHelper.GetText();
                if (LastText != content) RaiseClipboardChanged();
            }
        }

        return IntPtr.Zero;
    }

    private void RaiseClipboardChanged()
    {
        LastText = ClipboardHelper.GetText();
        actionWithString?.Invoke(LastText);
    }

    /// <summary>
    /// Registers the callback invoked whenever the clipboard content changes.
    /// Must be a method instead of an event: attaching an event handler from multiple
    /// user controls would otherwise call the handler once per registered control.
    /// </summary>
    /// <param name="handler">The callback receiving the new clipboard text.</param>
    public void SetHandler(Action<string> handler)
    {
        actionWithString = handler;
    }
}
#endif
