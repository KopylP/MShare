using MassTransit;
using MShare.Framework.Api;
using MShare.Framework.Application;
using MShare.Songs.Api.Messages;
using MShare.Framework.Infrastructure.Execution;
using MShare.Framework.Domain;
using MShare.Songs.Domain;
using MShare.Songs.Domain.Specifications;
using MShare.Framework.Types.Addresses;

namespace MShare.Songs.Application.Consumers.Messages.SaveAlbum
{
	public class EventConsumer : IIntegrationEventConsumer<UnsavedAlbumRequestedEvent>, IConsumer<UnsavedAlbumRequestedEvent>
    {
        private readonly IConsumerContextExecutor _executor;
        private readonly IUnitOfWorkProvider _provider;
        private readonly IAlbumRepository _albumRepository;

        public EventConsumer(IConsumerContextExecutor executor, IUnitOfWorkProvider provider, IAlbumRepository albumRepository)
        {
            _executor = executor;
            _provider = provider;
            _albumRepository = albumRepository;
        }

        public async Task Consume(ConsumeContext<UnsavedAlbumRequestedEvent> context)
        {
            using var uow = _provider.Create();

            var @event = context.Message;

            var album = await _albumRepository.FirstOrDefaultAsync(AlbumByIdSpecification.Of(@event.SourceId, @event.ServiceType));

            if (album is null)
            {
                album = new AlbumEntity(@event.SourceId, @event.ServiceType, CountryCode3.Of(@event.Country))
                {
                    Name = @event.Name,
                    ArtistName = @event.ArtistName,
                    SourceUrl = @event.SourceUrl,
                    ImageThumbnailUrl = @event.ImageThumbnailUrl,
                    ImageUrl = @event.ImageUrl
                };

                await _albumRepository.SaveAsync(album);

                await uow.CommitAsync();
            }
        }
    }
}

