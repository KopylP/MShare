using System;
using AutoMapper;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Application.Shared.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
            CreateMap<(ProxyService.Client.SongResponseDto, StreamingServiceType), SongResponseDto>()
                .ForMember(p => p.SongUrl, o => o.MapFrom(p => p.Item1.Song.SourceUrl))
                .ForMember(p => p.SongSourceId, o => o.MapFrom(p => p.Item1.Song.SourceId))
                .ForMember(p => p.AlbumName, o => o.MapFrom(p => p.Item1.Album.Name))
                .ForMember(p => p.SongName, o => o.MapFrom(p => p.Item1.Song.Name))
                .ForMember(p => p.CoverImageUrl, o => o.MapFrom(p => p.Item1.Album.ImageUrl))
                .ForMember(p => p.ArtistName, o => o.MapFrom(p => string.Join(" & ", p.Item1.Artists.Select(p => p.Name.Trim()))))
                .ForMember(p => p.ServiceType, o => o.MapFrom(p => p.Item2));

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