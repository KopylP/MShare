using System;
using AutoMapper;
using MShare.Songs.Api.Commands.V1;
using MShare.Songs.Api.Messages;

namespace MShare.Songs.Application.Consumers.Messages.SaveAlbum
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<UnsavedAlbumRequestedEvent, SaveAlbumCommand>();
		}
	}
}

