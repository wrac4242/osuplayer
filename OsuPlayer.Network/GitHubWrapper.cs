using Octokit;

namespace OsuPlayer.Network;

public static class GitHubWrapper
{
    /// <summary>
    ///     Gets the latest stable release from github!
    /// </summary>
    /// <returns>Returns a GitHub <see cref="Release" /></returns>
    public static async Task<Release?> GetLatestRelease()
    {
        var github = new GitHubClient(new ProductHeaderValue("osu!player"));

        var releases = await github.Repository.Release.GetAll("Founntain", "osuplayer");

        return releases.FirstOrDefault(x => !x.Prerelease);
    }

    /// <summary>
    ///     Gets the latest release from GitHub including Pre-Releases!
    /// </summary>
    /// <returns>Returns a GitHub <see cref="Release" /></returns>
    public static async Task<Release?> GetLatestPreRelease()
    {
        var github = new GitHubClient(new ProductHeaderValue("osu!player"));

        var releases = await github.Repository.Release.GetAll("Founntain", "osuplayer");

        return releases.FirstOrDefault();
    }
}