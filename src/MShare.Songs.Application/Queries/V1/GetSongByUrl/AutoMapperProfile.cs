using System;
using AutoMapper;
using MShare.Songs.Api.Queries.Dtos.V1;
using ProxyService.Client;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ServicesResponseDto.ItemDto, SongByUrlResponseDto.ServiceDto>();
			CreateMap<(SongResponseDto, ServicesResponseDto), SongByUrlResponseDto>()
				.ForMember(p => p.SongUrl, o => o.MapFrom(p => p.Item1.Song.SourceUrl))
				.ForMember(p => p.SongSourceId, o => o.MapFrom(p => p.Item1.Song.SourceId))
                .ForMember(p => p.AlbumName, o => o.MapFrom(p => p.Item1.Album.Name))
				.ForMember(p => p.SongName, o => o.MapFrom(p => p.Item1.Song.Name))
				.ForMember(p => p.CoverImageUrl, o => o.MapFrom(p => p.Item1.Album.ImageUrl))
				.ForMember(p => p.ArtistName, o => o.MapFrom(p => string.Join(" & ", p.Item1.Artists.Select(p => p.Name.Trim()))))
				.ForMember(p => p.Services, o => o.MapFrom(p => p.Item2.Items));
        }
	}
}

