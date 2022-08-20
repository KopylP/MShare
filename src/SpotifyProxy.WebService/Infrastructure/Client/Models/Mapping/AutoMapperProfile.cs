using AutoMapper;
using Proxy.Api;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models.Mapping
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SpotifyTrackResponseModel, SongResponseDto>().ConvertUsing<SongResponseConverter>();
            CreateMap<SpotifySearchResponseModel, SongsResponseDto>()
                .ForMember(p => p.Items, o => o.MapFrom(p => p.Tracks.Items ?? Array.Empty<SpotifyTrackResponseModel>()));
        }

        private class SongResponseConverter : ITypeConverter<SpotifyTrackResponseModel, SongResponseDto>
        {
            public SongResponseDto Convert(SpotifyTrackResponseModel source, SongResponseDto destination, ResolutionContext context)
            {
                return new SongResponseDto
                {
                    Song = SongSourceDto.Of(source.Name, source.Id, source.ExternalUrls.Spotify),
                    Album = AlbumSourceDto.Of(source.Album.Name, source.Album.Id, source.Album.Images.First().Url, source.Album.Images.Last().Url),
                    Artists = source.Artists.Select(p => ArtistSourceDto.Of(p.Name, p.Id)).ToArray()
                };
            }
        }
    }
}

