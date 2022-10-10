﻿using OsuPlayer.Data.OsuPlayer.Enums;

namespace OsuPlayer.Modules.Audio.Interfaces;

/// <summary>
/// This interface is used a service provider for shuffling songs.
/// </summary>
public interface IShuffleProvider
{
    /// <summary>
    /// Inits the shuffler with the given parameters.
    /// </summary>
    /// <param name="maxRange">The max index range the shuffler will return with <see cref="DoShuffle"/></param>
    void Init(int maxRange);

    /// <summary>
    /// Provides a method to generate a shuffled index based on simple parameters.
    /// </summary>
    /// <param name="currentIndex">The current song index the shuffle algorithm is based on</param>
    /// <param name="direction">The <see cref="ShuffleDirection" /> to shuffle to</param>
    /// <returns>a random generated (shuffled) index</returns>
    int DoShuffle(int currentIndex, ShuffleDirection direction);
}