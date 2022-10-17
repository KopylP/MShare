using AutoMapper;
using Proxy.Api;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models.Mapping
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<(SpotifyTrackResponseModel Song, SpotifyAlbumResponseModel Album), SongResponseDto>().ConvertUsing<SongResponseConverter>();
            CreateMap<SpotifyArtistResponseModel, ArtistSourceDto>()
                .ConvertUsing<ArtistResponseConverter>();
            CreateMap<SpotifyAlbumResponseModel, AlbumSourceDto>()
                .ConvertUsing<AlbumConverter>();
            CreateMap<SpotifySearchTrackResponseModel, SongsResponseDto>()
                .ForMember(p => p.Items, o => o.MapFrom(p => p.Tracks.Items ?? Array.Empty<SpotifyTrackResponseModel>()));
            CreateMap<SpotifyAlbumResponseModel, AlbumResponseDto>()
                .ConvertUsing<AlbumResponseConverter>();
        }

        private class SongResponseConverter : ITypeConverter<(SpotifyTrackResponseModel Song, SpotifyAlbumResponseModel Album), SongResponseDto>
        {
            public SongResponseDto Convert((SpotifyTrackResponseModel Song, SpotifyAlbumResponseModel Album) source, SongResponseDto destination, ResolutionContext context)
            {
                return new SongResponseDto
                {
                    Song = SongSourceDto.Of(source.Song.Name, source.Song.Id, source.Song.ExternalUrls.Spotify, source.Song.ExternalIds.Isrc),
                    Album = context.Mapper.Map<AlbumSourceDto>(source.Album),
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

        private class AlbumConverter : ITypeConverter<SpotifyAlbumResponseModel, AlbumSourceDto>
        {
            public AlbumSourceDto Convert(SpotifyAlbumResponseModel source, AlbumSourceDto destination, ResolutionContext context)
            {
                return new AlbumSourceDto()
                {
                    Name = source.Name,
                    SourceId = source.Id,
                    SourceUrl = source.ExternalUrls.Spotify,
                    Region = Region.Invariant,
                    ImageUrl = source.Images.First().Url,
                    ImageThumbnailUrl = source.Images.Last().Url,
                    Upc = source.ExternalIds.Upc ?? ""
                };
            }
        }

        private class AlbumResponseConverter : ITypeConverter<SpotifyAlbumResponseModel, AlbumResponseDto>
        {
            public AlbumResponseDto Convert(SpotifyAlbumResponseModel source, AlbumResponseDto destination, ResolutionContext context)
            {
                return new AlbumResponseDto
                {
                    Album = context.Mapper.Map<AlbumSourceDto>(source),
                    Artists = context.Mapper.Map<ArtistSourceDto[]>(source.Artists)
                };
            }
        }
    }
}

