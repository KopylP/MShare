namespace SpotifyProxy.WebService.Helpers
{
    public static class StringExtentions
    {
        public static string GetSpotifySongId(this string url)
        {
            // Spotify link example
            // https://open.spotify.com/track/56wSomSllGESHGhcfHrvEw
            return url.Replace("//", "/")
                .Split("/")[3];
        }
    }
}

