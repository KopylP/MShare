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
                    Song = SongSourceDto.Of(source.Track.TrackName, source.Track.TrackId, source.Track.TrackViewUrl, null, source.Region),
                    Album = new()
                    {
                        Name = source.Track.CollectionName,
                        SourceId = source.Track.CollectionId,
                        SourceUrl = source.Track.CollectionViewUrl.RemoveFrom('?'),
                        Region = source.Region,
                        ImageThumbnailUrl = source.Track.ArtworkUrl100.GetApplePhotoSizeUrl(64),
                        ImageUrl = source.Track.ArtworkUrl100.GetApplePhotoSizeUrl(640)
                    },
                    Artists = new ArtistSourceDto[]
                    {
                        ArtistSourceDto.Of(source.Track.ArtistName, source.Track.CollectionId)
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
                        Name = source.Album.CollectionName,
                        SourceId = source.Album.CollectionId,
                        SourceUrl = source.Album.CollectionViewUrl.RemoveFrom('?'),
                        Region = source.Region,
                        ImageThumbnailUrl = source.Album.ArtworkUrl100.GetApplePhotoSizeUrl(64),
                        ImageUrl = source.Album.ArtworkUrl100.GetApplePhotoSizeUrl(640)
                    },
                    Artists = new ArtistSourceDto[]
                    {
                        ArtistSourceDto.Of(source.Album.ArtistName, source.Album.CollectionId)
                    },
                };
            }
        }
    }
}

