namespace SpotifyProxy.WebService.Helpers
{
    public static class StringExtentions
    {
        public static string GetId(this string url)
        {
            // Spotify link example
            // https://open.spotify.com/track/56wSomSllGESHGhcfHrvEw
            return url.Replace("//", "/")
                .Split("/")[3]
                .RemoveFrom('?');
        }
    }
}

