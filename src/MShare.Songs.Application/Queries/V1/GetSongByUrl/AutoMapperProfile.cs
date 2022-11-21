﻿using System;
using AutoMapper;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Events;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
            CreateMap<QueryContext, UnsavedSongRequestedEvent>()
                .ForMember(p => p.Name, o => o.MapFrom(p => p.ServiceProxyResponse.Song.Name))
                .ForMember(p => p.ArtistName, o => o.MapFrom(p => string.Join(" & ", p.ServiceProxyResponse.Artists.Select(p => p.Name))))
                .ForMember(p => p.ImageUrl, o => o.MapFrom(p => p.ServiceProxyResponse.Album.ImageUrl))
                .ForMember(p => p.ImageThumbnailUrl, o => o.MapFrom(p => p.ServiceProxyResponse.Album.ImageThumbnailUrl))
                .ForMember(p => p.ServiceType, o => o.MapFrom(p => p.ServiceType))
                .ForMember(p => p.SourceId, o => o.MapFrom(p => p.ServiceProxyResponse.Song.SourceId))
                .ForMember(p => p.SourceUrl, o => o.MapFrom(p => p.ServiceProxyResponse.Song.SourceUrl))
                .ForMember(p => p.Region, o => o.MapFrom(p => p.ServiceProxyResponse.Song.Region))
                .ForMember(p => p.AlbumName, o => o.MapFrom(p => p.ServiceProxyResponse.Album.Name))
                .ForMember(p => p.AlbumSourceId, o => o.MapFrom(p => p.ServiceProxyResponse.Album.SourceId))
                .ForMember(p => p.AlbumSourceUrl, o => o.MapFrom(p => p.ServiceProxyResponse.Album.SourceUrl))
                .ForMember(p => p.Isrc, o => o.MapFrom(p => p.ServiceProxyResponse.Song.Isrc))
                .ForMember(p => p.AlbumUpc, o => o.MapFrom(p => p.ServiceProxyResponse.Album.Upc));
        }
	}
}

