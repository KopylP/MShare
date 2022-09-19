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
        }

        private class SongResponseConverter : ITypeConverter<AppleTrackResponseModel, SongResponseDto>
        {
            public SongResponseDto Convert(AppleTrackResponseModel source, SongResponseDto destination, ResolutionContext context)
            {
                return new SongResponseDto
                {
                    Song = SongSourceDto.Of(source.TrackName, source.TrackId, source.TrackViewUrl, source.Country),
                    Album = AlbumSourceDto.Of(
                        source.CollectionName,
                        source.CollectionId,
                        source.ArtworkUrl100.GetApplePhotoSizeUrl(640),
                        source.ArtworkUrl100.GetApplePhotoSizeUrl(64)),
                    Artists = new ArtistSourceDto[]
                    {
                        ArtistSourceDto.Of(source.ArtistName, source.CollectionId)
                    }
                };
            }
        }

    }
}

