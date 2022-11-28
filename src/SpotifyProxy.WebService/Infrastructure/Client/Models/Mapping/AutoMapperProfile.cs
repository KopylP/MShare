using AutoMapper;
using Proxy.Api;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models.Mapping
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<(SpotifyTrackResponseModel Song, SpotifyAlbumResponseModel Album, string Region), SongResponseDto>().ConvertUsing<SongResponseConverter>();
            CreateMap<SpotifyArtistResponseModel, ArtistSourceDto>()
                .ConvertUsing<ArtistResponseConverter>();
            CreateMap<(SpotifyAlbumResponseModel, string), AlbumSourceDto>()
                .ConvertUsing<AlbumConverter>();
            CreateMap<(SpotifyAlbumResponseModel Album, string Region), AlbumResponseDto>()
                .ConvertUsing<AlbumResponseConverter>();
        }

        private class SongResponseConverter : ITypeConverter<(SpotifyTrackResponseModel Song, SpotifyAlbumResponseModel Album, string Region), SongResponseDto>
        {
            public SongResponseDto Convert((SpotifyTrackResponseModel Song, SpotifyAlbumResponseModel Album, string Region) source, SongResponseDto destination, ResolutionContext context)
            {
                return new SongResponseDto
                {
                    Song = SongSourceDto.Of(source.Song.Name, source.Song.Id, source.Song.ExternalUrls.Spotify, source.Song.ExternalIds.Isrc, source.Region),
                    Album = context.Mapper.Map<AlbumSourceDto>((source.Album, source.Region)),
                    Artists = context.Mapper.Map<ArtistSourceDto[]>(source.Song.Artists)
                };
            }
        }

        private class ArtistResponseConverter : ITypeConverter<SpotifyArtistResponseModel, ArtistSourceDto>
        {
            public ArtistSourceDto Convert(SpotifyArtistResponseModel source, ArtistSourceDto destination, ResolutionContext context)
            {
                return ArtistSourceDto.Of(source.Name, source.Id);
            }
        }

        private class AlbumConverter : ITypeConverter<(SpotifyAlbumResponseModel Album, string Region), AlbumSourceDto>
        {
            public AlbumSourceDto Convert((SpotifyAlbumResponseModel Album, string Region) source, AlbumSourceDto destination, ResolutionContext context)
            {
                return new AlbumSourceDto()
                {
                    Name = source.Album.Name,
                    SourceId = source.Album.Id,
                    SourceUrl = source.Album.ExternalUrls.Spotify,
                    Region = source.Region,
                    ImageUrl = source.Album.Images.First().Url,
                    ImageThumbnailUrl = source.Album.Images.Last().Url,
                    Upc = source.Album.ExternalIds.Upc ?? "",
                };
            }
        }

        private class AlbumResponseConverter : ITypeConverter<(SpotifyAlbumResponseModel Album, string Region), AlbumResponseDto>
        {
            public AlbumResponseDto Convert((SpotifyAlbumResponseModel Album, string Region) source, AlbumResponseDto destination, ResolutionContext context)
            {
                return new AlbumResponseDto
                {
                    Album = context.Mapper.Map<AlbumSourceDto>((source.Album, source.Region)),
                    Artists = context.Mapper.Map<ArtistSourceDto[]>(source.Album.Artists)
                };
            }
        }
    }
}

