using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Input;

namespace OsuPlayer.Modules.Hotkeys;

public class HotkeyWindowsImplementation : IHotkeyImplementation
{
    public List<Hotkey> Hotkeys { get; set; }
    public IntPtr Handle { get; set; }

    public void InitializeHotkeys()
    {
        foreach (var hotkey in Hotkeys)
        {
            var result = RegisterHotkey(hotkey);

            Debug.WriteLine($"Hotkey {hotkey.Key.ToString()} {(result ? string.Empty : "not ")}successfully registered!");
        }
    }

    public void DeInitializeHotkeys()
    {
        foreach (var hotkey in Hotkeys) UnregisterHotkey(hotkey);
    }

    public bool RegisterHotkey(Hotkey hotkey)
    {
        return RegisterHotKey(IntPtr.Zero, hotkey.Id, (int) hotkey.ModifierKey | 0x4000, (int) hotkey.Key);
    }

    public bool UnregisterHotkey(Hotkey hotkey)
    {
        return UnregisterHotKey(IntPtr.Zero, hotkey.Id);
    }

    public void MessageHandleLoop()
    {
        var message = new WinMessage();

        while (GetMessage(ref message, IntPtr.Zero, 0, 0))
            if (message.message == 0x0312)
            {
                var key = (WinKey) (((int) message.lParam >> 16) & 0xFFFF);
                var modifiers = (KeyModifiers) ((int) message.lParam & 0xFFFF);

                for (var i = 0; i < Hotkeys.Count; i++)
                {
                    var x = Hotkeys[i];

                    if (key == x.Key) x.Command.Invoke();
                }
            }
    }

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("user32")]
    private static extern bool GetMessage(ref WinMessage winMessage, IntPtr handle, uint mMsgFilterInMain, uint mMsgFilterMax);
}