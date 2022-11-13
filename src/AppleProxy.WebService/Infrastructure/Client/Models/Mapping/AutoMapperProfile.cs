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
                .ForMember(p => p.Items, o => o.MapFrom(p => p.Data));
			CreateMap<(AppleTrackResponseModel, string), SongResponseDto>()
				.ConvertUsing<SongResponseConverter>();
            CreateMap<(AppleAlbumResponseModel, string), AlbumResponseDto>()
                .ConvertUsing<AlbumResponseConverter>();
        }

        private class SongResponseConverter : ITypeConverter<(AppleTrackResponseModel Track, string Region), SongResponseDto>
        {
            public SongResponseDto Convert((AppleTrackResponseModel Track, string Region) source, SongResponseDto destination, ResolutionContext context)
            {
                return new SongResponseDto
                {
                    Song = SongSourceDto.Of(source.Track.Attributes.Name, source.Track.Id, source.Track.Attributes.Url, source.Track.Attributes.Isrc, source.Region),
                    Artists = new ArtistSourceDto[]
                    {
                        ArtistSourceDto.Of(source.Track.Attributes.ArtistName, source.Track.Relationships.Artists?.Data?.First()?.Id ?? "")
                    },
                };
            }
        }

        private class AlbumResponseConverter : ITypeConverter<(AppleAlbumResponseModel Album, string Region), AlbumResponseDto>
        {
            public AlbumResponseDto Convert((AppleAlbumResponseModel Album, string Region) source, AlbumResponseDto destination, ResolutionContext context)
            {
                return new AlbumResponseDto
                {
                    Album = new()
                    {
                        Name = source.Album.Attributes.Name,
                        SourceId = source.Album.Id,
                        SourceUrl = source.Album.Attributes.Url.RemoveFrom('?'),
                        Region = source.Region,
                        ImageThumbnailUrl = source.Album.Attributes.Artwork.Url.GetApplePhotoSizeUrl(64),
                        ImageUrl = source.Album.Attributes.Artwork.Url.GetApplePhotoSizeUrl(640),
                        Upc = source.Album.Attributes.Upc
                    },
                    Artists = new ArtistSourceDto[]
                    {
                        ArtistSourceDto.Of(source.Album.Attributes.ArtistName, source.Album.Relationships.Artists?.Data?.FirstOrDefault()?.Id ?? "")
                    },
                };
            }
        }
    }
}

