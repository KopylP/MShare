using System;
using AutoMapper;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Messages;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Application.Queries.V1.GetAlbumByUrl
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<(ProxyService.Client.AlbumResponseDto, StreamingServiceType), AlbumResponseDto>()
                .ForMember(p => p.AlbumUrl, o => o.MapFrom(p => p.Item1.Album.SourceUrl))
                .ForMember(p => p.AlbumSourceId, o => o.MapFrom(p => p.Item1.Album.SourceId))
                .ForMember(p => p.AlbumName, o => o.MapFrom(p => p.Item1.Album.Name))
                .ForMember(p => p.AlbumName, o => o.MapFrom(p => p.Item1.Album.Name))
                .ForMember(p => p.CoverImageUrl, o => o.MapFrom(p => p.Item1.Album.ImageUrl))
                .ForMember(p => p.ArtistName, o => o.MapFrom(p => string.Join(" & ", p.Item1.Artists.Select(p => p.Name.Trim()))))
                .ForMember(p => p.ServiceType, o => o.MapFrom(p => p.Item2));

            CreateMap<QueryContext, UnsavedAlbumRequestedEvent>()
                .ForMember(p => p.Name, o => o.MapFrom(p => p.ServiceProxyResponse.Album.Name))
                .ForMember(p => p.ArtistName, o => o.MapFrom(p => string.Join(" & ", p.ServiceProxyResponse.Artists.Select(p => p.Name))))
                .ForMember(p => p.ImageUrl, o => o.MapFrom(p => p.ServiceProxyResponse.Album.ImageUrl))
                .ForMember(p => p.ImageThumbnailUrl, o => o.MapFrom(p => p.ServiceProxyResponse.Album.ImageThumbnailUrl))
                .ForMember(p => p.ServiceType, o => o.MapFrom(p => p.ServiceType))
                .ForMember(p => p.SourceId, o => o.MapFrom(p => p.ServiceProxyResponse.Album.SourceId))
                .ForMember(p => p.SourceUrl, o => o.MapFrom(p => p.ServiceProxyResponse.Album.SourceUrl))
                .ForMember(p => p.Country, o => o.MapFrom(p => p.ServiceProxyResponse.Album.Country));
        }
	}
}

