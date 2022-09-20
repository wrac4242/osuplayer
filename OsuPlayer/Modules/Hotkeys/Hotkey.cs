namespace OsuPlayer.Modules.Hotkeys;

public sealed class Hotkey
{
    public Hotkey(int id, WinKey key, Action command)
    {
        Id = id;
        Key = key;
        Command = command;
    }

    public Hotkey(int id, WinKey key, Action command, ModifierKey modifierKey)
    {
        Id = id;
        Key = key;
        Command = command;
        ModifierKey = modifierKey;
    }

    public int Id { get; }
    public WinKey Key { get; set; }
    public Action Command { get; }
    public ModifierKey ModifierKey { get; } = ModifierKey.None;

    public void RunCommand()
    {
        Command();
    }
}