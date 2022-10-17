using System;
using AutoMapper;
using MShare.Songs.Api.Commands.V1;
using MShare.Songs.Api.Events;

namespace MShare.Songs.Application.Consumers.Messages.SaveSong
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<UnsavedSongRequestedEvent, SaveAlbumCommand>()
				.ForMember(p => p.Name, o => o.MapFrom(p => p.AlbumName))
				.ForMember(p => p.SourceId, o => o.MapFrom(p => p.AlbumSourceId))
				.ForMember(p => p.SourceUrl, o => o.MapFrom(p => p.AlbumSourceUrl))
				.ForMember(p => p.Upc, o => o.MapFrom(p => p.AlbumUpc));


            CreateMap<UnsavedSongRequestedEvent, SaveSongCommand>();
        }
	}
}