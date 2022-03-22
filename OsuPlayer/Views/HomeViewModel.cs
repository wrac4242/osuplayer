using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OsuPlayer.IO.DbReader;
using OsuPlayer.IO.Storage.Config;
using OsuPlayer.Network.API.ApiEndpoints;
using OsuPlayer.Network.Online;
using OsuPlayer.ViewModels;
using ReactiveUI;
using SkiaSharp;

namespace OsuPlayer.Views;

public class HomeViewModel : BaseViewModel
{
    public ISeries[] Series { get; set; }
        =
        {
            new ColumnSeries<double>
            {
                Values = new double[] {2, 1, 3, 5, 3, 4, 6},
                Fill = new SolidColorPaint(SKColors.Purple)
            }
        };

    public Axis[] XAxes { get; set; }
        =
        {
            new()
            {
                Labels = null,

                LabelsRotation = 15
            }
        };

    public HomeViewModel()
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(Block);
    }

    private async void Block(CompositeDisposable disposables)
    {
        Disposable.Create(() => { }).DisposeWith(disposables);

        ProfilePicture = await LoadProfilePicture();
    }

    public List<MapEntry> SongEntries => Core.Instance.Player.SongSource!;

    private bool _songsLoading;
    private Bitmap? _profilePicture;

    public bool IsUserNotLoggedIn => CurrentUser == default;
    public bool IsUserLoggedIn => CurrentUser != default;

    public bool SongsLoading
    {
        get => new Config().Read().OsuPath != null && _songsLoading;
        set => this.RaiseAndSetIfChanged(ref _songsLoading, value);
    }

    public User? CurrentUser => ProfileManager.User;

    public Bitmap? ProfilePicture
    {
        get => _profilePicture;
        set => this.RaiseAndSetIfChanged(ref _profilePicture, value);
    }

    internal async Task<Bitmap?> LoadProfilePicture()
    {
        if (CurrentUser == default) return default;

        var profilePicture = await ApiAsync.GetProfilePictureAsync(CurrentUser.Name);

        if (profilePicture == default) return default;

        await using (var stream = new MemoryStream(Convert.FromBase64String(profilePicture)))
        {
            return await Task.Run(() => new Bitmap(stream));
        }
    }
}