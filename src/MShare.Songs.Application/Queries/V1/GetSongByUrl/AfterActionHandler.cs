using System;
using AutoMapper;
using MShare.Framework.Application;
using MShare.Framework.Application.Actions;
using MShare.Framework.Application.Context;
using MShare.Songs.Api.Events;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
	public class AfterActionHandler : IAfterActionHandler<GetSongByUrlQuery, SongResponseDto>
    {
        private readonly QueryContext _context;
        private readonly IIntegrationBus _integrationBus;
        private readonly IMapper _mapper;

        public AfterActionHandler(IRequestContext<GetSongByUrlQuery, SongResponseDto> context, IIntegrationBus integrationBus, IMapper mapper)
        {
            _context = (QueryContext)context;
            _integrationBus = integrationBus;
            _mapper = mapper;
        }

        public async Task Handle(GetSongByUrlQuery request, SongResponseDto resonse)
        {
            if (_context.ServiceProxyResponse is not null)
            {
                await _integrationBus.Publish(_mapper.Map<UnsavedSongRequestedEvent>(_context));
            }
        }
    }
}