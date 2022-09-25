using System;
using AutoMapper;
using MShare.Songs.Abstractions;
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
        }
	}
}

