﻿using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using OsuPlayer.Data.API.Models.Beatmap;
using OsuPlayer.Extensions;

namespace OsuPlayer.Views;

public partial class UserView : ReactiveControl<UserViewModel>
{
    public UserView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void UserList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var viewer = this.FindControl<ContentPresenter>("BadgeItems");
        var list = (ListBox) sender;

        if (list?.SelectedItem == default) return;

        var items = ViewModel!.LoadBadges((User) list.SelectedItem);

        viewer.Content = new ItemsRepeater
        {
            Items = items,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Layout = new WrapLayout
            {
                Orientation = Orientation.Horizontal
            }
        };

        viewer.UpdateChild();
    }

    private async void UserTopSongsList_OnDoubleTapped(object? sender, RoutedEventArgs e)
    {
        var listBox = (ListBox) sender;

        if (listBox == default) return;

        var beatmapModel = (BeatmapUserValidityModel) listBox.SelectedItem;

        if (beatmapModel == default || ViewModel.Player.SongSourceProvider.SongSourceList == default) return;

        var mapEntry = ViewModel.Player.SongSourceProvider.SongSourceList.FirstOrDefault(x =>
            x.BeatmapSetId == beatmapModel.Beatmap.BeatmapSetId ||
            (x.Artist == beatmapModel.Beatmap.Artist && x.Title == beatmapModel.Beatmap.Title));

        if (mapEntry != default)
            await ViewModel.Player.TryPlaySongAsync(mapEntry);
    }

    private void WebProfile_OnClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedUser == default) return;

        GeneralExtensions.OpenUrl($"https://stats.founntain.dev/user/{Uri.EscapeDataString(ViewModel.SelectedUser.Name)}");
    }

    private void OsuProfile_OnClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedUser == default) return;

        GeneralExtensions.OpenUrl($"https://osu.ppy.sh/users/{ViewModel.SelectedUser.OsuProfile}");
    }
}