using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using OsuPlayer.Data.OsuPlayer.Enums;
using OsuPlayer.Data.OsuPlayer.StorageModels;
using OsuPlayer.Extensions;
using OsuPlayer.IO.Storage.Blacklist;
using OsuPlayer.IO.Storage.Playlists;
using OsuPlayer.Modules.Audio.Interfaces;
using OsuPlayer.Windows;
using ReactiveUI;
using Splat;

namespace OsuPlayer.Views;

public partial class PlayerControlView : ReactiveControl<PlayerControlViewModel>
{
    public MainWindow _mainWindow;

    private Slider ProgressSlider => this.FindControl<Slider>("SongProgressSlider");
    private Button RepeatButton => this.FindControl<Button>("Repeat");

    public PlayerControlView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables =>
        {
            if (this.GetVisualRoot() is MainWindow mainWindow)
                _mainWindow = mainWindow;

            ProgressSlider.AddHandler(PointerPressedEvent, SongProgressSlider_OnPointerPressed,
                RoutingStrategies.Tunnel);

            ProgressSlider.AddHandler(PointerReleasedEvent, SongProgressSlider_OnPointerReleased,
                RoutingStrategies.Tunnel);

            RepeatButton.AddHandler(PointerReleasedEvent, Repeat_OnPointerReleased, RoutingStrategies.Tunnel);

            ViewModel.RaisePropertyChanged(nameof(ViewModel.IsAPlaylistSelected));
            ViewModel.RaisePropertyChanged(nameof(ViewModel.IsCurrentSongInPlaylist));
            ViewModel.RaisePropertyChanged(nameof(ViewModel.IsCurrentSongOnBlacklist));
        });
        AvaloniaXamlLoader.Load(this);
    }

    private void Repeat_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Right)
            ViewModel.RaisePropertyChanged(nameof(ViewModel.Playlists));
    }

    private void SongProgressSlider_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        ViewModel.Player.Play();
    }

    private void SongProgressSlider_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        ViewModel.Player.Pause();
    }

    private void Settings_OnClick(object? sender, RoutedEventArgs e)
    {
        _mainWindow.ViewModel!.MainView = _mainWindow.ViewModel.SettingsView;
        _mainWindow.ViewModel!.IsPaneOpen = true;
    }

    private async void Blacklist_OnClick(object? sender, RoutedEventArgs e)
    {
        await using (var blacklist = new Blacklist())
        {
            await blacklist.ReadAsync();

            var currentHash = ViewModel.CurrentSong.Value?.Hash;

            if (blacklist.Contains(ViewModel.CurrentSong.Value))
            {
                blacklist.Container.Songs.Remove(currentHash);
            }
            else
            {
                blacklist.Container.Songs.Add(currentHash);

                if (ViewModel.Player.BlacklistSkip.Value)
                    ViewModel.Player.NextSong(PlayDirection.Forward);
            }
        }

        ViewModel.Player.TriggerBlacklistChanged(new PropertyChangedEventArgs("Black"));
        ViewModel.RaisePropertyChanged(nameof(ViewModel.IsCurrentSongOnBlacklist));
    }

    private async void Favorite_OnClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.Player.CurrentSong.Value != null)
        {
            await PlaylistManager.ToggleSongOfCurrentPlaylist(ViewModel.Player.SelectedPlaylist.Value, ViewModel.Player.CurrentSong.Value);
            ViewModel.Player.TriggerPlaylistChanged(new PropertyChangedEventArgs("Fav"));
        }

        ViewModel.RaisePropertyChanged(nameof(ViewModel.IsCurrentSongInPlaylist));
    }

    private void SongControl(object? sender, RoutedEventArgs e)
    {
        switch ((sender as Control)?.Name)
        {
            case "Repeat":
                ViewModel.Player.RepeatMode.Value = ViewModel.Player.RepeatMode.Value.Next();
                break;
            case "Previous":
                ViewModel.Player.NextSong(PlayDirection.Backwards);
                break;
            case "PlayPause":
                ViewModel.Player.PlayPause();
                break;
            case "Next":
                ViewModel.Player.NextSong(PlayDirection.Forward);
                break;
            case "Shuffle":
                ViewModel.IsShuffle = !ViewModel.IsShuffle;
                break;
        }
    }

    private void Volume_OnClick(object? sender, RoutedEventArgs e)
    {
        ViewModel.Player.ToggleMute();
    }

    private void PlaybackSpeed_OnClick(object? sender, RoutedEventArgs e)
    {
        ViewModel!.PlaybackSpeed = 0;
    }

    private void RepeatContextMenu_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        ViewModel.Player.SelectedPlaylist.Value = (Playlist) (sender as ContextMenu)?.SelectedItem;
    }

    private void OpenMiniPlayer_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_mainWindow.Miniplayer != null)
            return;

        _mainWindow.Miniplayer = new Miniplayer(_mainWindow, ViewModel.Player, Locator.Current.GetRequiredService<IAudioEngine>());

        _mainWindow.Miniplayer.Show();

        _mainWindow.WindowState = WindowState.Minimized;
    }
}