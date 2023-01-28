using MassTransit;
using MShare.Framework.Api;
using MShare.Framework.Application;
using MShare.Songs.Api.Messages;
using MShare.Framework.Infrastructure.Execution;
using MShare.Framework.Domain;
using MShare.Songs.Domain;
using MShare.Songs.Domain.Specifications;
using MShare.Framework.Types.Addresses;
using AutoMapper;
using MShare.Songs.Api.Commands.V1;

namespace MShare.Songs.Application.Consumers.Messages.SaveAlbum
{
	public class EventConsumer : IIntegrationEventConsumer<UnsavedAlbumRequestedEvent>
    {
        private readonly IConsumerContextExecutor _executor;
        private readonly IMapper _mapper;

        public EventConsumer(IConsumerContextExecutor executor, IMapper mapper)
        {
            _executor = executor;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UnsavedAlbumRequestedEvent> context)
        {
            await _executor.ExecuteAsync(_mapper.Map<SaveAlbumCommand>(context.Message), context);
        }
    }
}

