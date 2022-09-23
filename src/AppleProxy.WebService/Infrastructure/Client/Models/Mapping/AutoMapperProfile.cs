using AppleProxy.WebService.Helpers;
using AutoMapper;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client.Models.Mapping
{
	internal class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
            CreateMap<AppleTrackListResponseModel, SongsResponseDto>()
                .ForMember(p => p.Items, o => o.MapFrom(p => p.Results));
			CreateMap<AppleTrackResponseModel, SongResponseDto>()
				.ConvertUsing<SongResponseConverter>();
            CreateMap<AppleAlbumResponseModel, AlbumResponseDto>()
                .ConvertUsing<AlbumResponseConverter>();
        }

        private class SongResponseConverter : ITypeConverter<AppleTrackResponseModel, SongResponseDto>
        {
            public SongResponseDto Convert(AppleTrackResponseModel source, SongResponseDto destination, ResolutionContext context)
            {
                return new SongResponseDto
                {
                    Song = SongSourceDto.Of(source.TrackName, source.TrackId, source.TrackViewUrl, source.Country),
                    Album = new()
                    {
                        Name = source.CollectionName,
                        SourceId = source.CollectionId,
                        SourceUrl = source.CollectionViewUrl.RemoveFrom('?'),
                        Country = source.Country,
                        ImageThumbnailUrl = source.ArtworkUrl100.GetApplePhotoSizeUrl(64),
                        ImageUrl = source.ArtworkUrl100.GetApplePhotoSizeUrl(640)
                    },
                    Artists = new ArtistSourceDto[]
                    {
                        ArtistSourceDto.Of(source.ArtistName, source.CollectionId)
                    },
                };
            }
        }

        private class AlbumResponseConverter : ITypeConverter<AppleAlbumResponseModel, AlbumResponseDto>
        {
            public AlbumResponseDto Convert(AppleAlbumResponseModel source, AlbumResponseDto destination, ResolutionContext context)
            {
                return new AlbumResponseDto
                {
                    Album = new()
                    {
                        Name = source.CollectionName,
                        SourceId = source.CollectionId,
                        SourceUrl = source.CollectionViewUrl.RemoveFrom('?'),
                        Country = source.Country,
                        ImageThumbnailUrl = source.ArtworkUrl100.GetApplePhotoSizeUrl(64),
                        ImageUrl = source.ArtworkUrl100.GetApplePhotoSizeUrl(640)
                    },
                    Artists = new ArtistSourceDto[]
                    {
                        ArtistSourceDto.Of(source.ArtistName, source.CollectionId)
                    },
                };
            }
        }
    }
}

