﻿using System;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OsuPlayer.IO.Storage.Config;
using OsuPlayer.ViewModels;
using ReactiveUI;

namespace OsuPlayer.Views;

public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }

    private void SettingsView_OnInitialized(object? sender, EventArgs e)
    {
        using var config = new Config();
        ViewModel!.OsuLocation = config.Read().OsuPath!;
    }
}