using AutoMapper;
using Proxy.Api;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models.Mapping
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SpotifyTrackResponseModel, SongResponseDto>().ConvertUsing<SongResponseConverter>();
            CreateMap<ArtistResponseModel, ArtistSourceDto>()
                .ConvertUsing<ArtistResponseConverter>();
            CreateMap<AlbumResponseModel, AlbumSourceDto>()
                .ConvertUsing<AlbumConverter>();
            CreateMap<SpotifySearchTrackResponseModel, SongsResponseDto>()
                .ForMember(p => p.Items, o => o.MapFrom(p => p.Tracks.Items ?? Array.Empty<SpotifyTrackResponseModel>()));
            CreateMap<AlbumResponseModel, AlbumResponseDto>()
                .ConvertUsing<AlbumResponseConverter>();
        }

        private class SongResponseConverter : ITypeConverter<SpotifyTrackResponseModel, SongResponseDto>
        {
            public SongResponseDto Convert(SpotifyTrackResponseModel source, SongResponseDto destination, ResolutionContext context)
            {
                return new SongResponseDto
                {
                    Song = SongSourceDto.Of(source.Name, source.Id, source.ExternalUrls.Spotify),
                    Album = context.Mapper.Map<AlbumSourceDto>(source.Album),
                    Artists = context.Mapper.Map<ArtistSourceDto[]>(source.Artists)
                };
            }
        }

        private class ArtistResponseConverter : ITypeConverter<ArtistResponseModel, ArtistSourceDto>
        {
            public ArtistSourceDto Convert(ArtistResponseModel source, ArtistSourceDto destination, ResolutionContext context)
            {
                return ArtistSourceDto.Of(source.Name, source.Id);
            }
        }

        private class AlbumConverter : ITypeConverter<AlbumResponseModel, AlbumSourceDto>
        {
            public AlbumSourceDto Convert(AlbumResponseModel source, AlbumSourceDto destination, ResolutionContext context)
            {
                return new AlbumSourceDto()
                {
                    Name = source.Name,
                    SourceId = source.Id,
                    SourceUrl = source.ExternalUrls.Spotify,
                    Country = Region.Invariant,
                    ImageUrl = source.Images.First().Url,
                    ImageThumbnailUrl = source.Images.Last().Url,
                };
            }
        }

        private class AlbumResponseConverter : ITypeConverter<AlbumResponseModel, AlbumResponseDto>
        {
            public AlbumResponseDto Convert(AlbumResponseModel source, AlbumResponseDto destination, ResolutionContext context)
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

