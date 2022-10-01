using MassTransit;
using MShare.Framework.Api;
using MShare.Framework.Application;
using MShare.Songs.Api.Messages;
using MShare.Framework.Infrastructure.Execution;

namespace MShare.Songs.Application.Consumers.Messages.SaveSong
{
	public class EventConsumer : IIntegrationEventConsumer<SongRequestedEvent>, IConsumer<SongRequestedEvent>
    {
        private readonly IConsumerContextExecutor _executor;

        public EventConsumer(IConsumerContextExecutor executor) => _executor = executor;

        public async Task Consume(ConsumeContext<SongRequestedEvent> context)
        {
            await Task.Delay(300);
            return;
        }
    }
}

