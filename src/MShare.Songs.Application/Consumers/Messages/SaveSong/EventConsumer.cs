using System;
using AutoMapper;
using MassTransit;
using MShare.Framework.Api;
using MShare.Framework.Application;
using MShare.Framework.Domain;
using MShare.Songs.Api.Commands.V1;
using MShare.Songs.Api.Events;
using MShare.Songs.Domain;

namespace MShare.Songs.Application.Consumers.Messages.SaveSong
{
	public class EventConsumer : IIntegrationEventConsumer<UnsavedSongRequestedEvent>
	{
        private readonly IConsumerContextExecutor _executor;
        private readonly IUnitOfWorkProvider _provider;
        private readonly IMapper _mapper;

        public EventConsumer(IConsumerContextExecutor executor, IMapper mapper, IUnitOfWorkProvider provider)
        {
            _executor = executor;
            _mapper = mapper;
            _provider = provider;
        }

        public async Task Consume(ConsumeContext<UnsavedSongRequestedEvent> context)
        {
            using (var uow = _provider.Create())
            {
                await _executor.ExecuteAsync(_mapper.Map<SaveAlbumCommand>(context.Message), context);
                await _executor.ExecuteAsync(_mapper.Map<SaveSongCommand>(context.Message), context);
            }
        }
    }
}