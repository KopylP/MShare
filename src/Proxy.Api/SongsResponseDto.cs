using System.Text.Json.Serialization;
using Unidecode.NET;

namespace Proxy.Api
{
    public record SongsResponseDto
    {
        public SongResponseDto[] Items { get; set; }

        [JsonIgnore]
        public bool IsEmpty => !Items?.Any() ?? true;
    }

    public static class SongResponseDtoExtentions
    {
        public static SongsResponseDto FilterItemsByArtistName(this SongsResponseDto response, string artistName)
        {
            if (!string.IsNullOrWhiteSpace(artistName) && response.Items is not null)
            {
                var lengthRange = (
                    Min: artistName.Unidecode().Length - 1,
                    Max: artistName.Unidecode().Length + 1);

                response.Items = response.Items.Where(item =>
                {
                    var artists = item.Artists.First().Name.Split("&").Select(p => p.Unidecode());
                    return artists.Any(artist => artist.Length >= lengthRange.Min && artist.Length <= lengthRange.Max);
                })
                .ToArray();
            }

            return response;
        }

        public static SongsResponseDto FilterItemsBySongName(this SongsResponseDto response, string songName)
        {
            if (!string.IsNullOrWhiteSpace(songName) && response is not null)
            {
                var items = response
                    .Items
                    .Where(item => item.Song.Name.ToLower() == songName.ToLower())
                    .ToArray();

                if (items.Any())
                {
                    response.Items = items;
                }
            }

            return response;
        }
    }
}

