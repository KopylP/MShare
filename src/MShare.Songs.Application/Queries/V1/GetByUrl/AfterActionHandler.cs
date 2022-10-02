using System;
using MShare.Framework.Application;
using MShare.Framework.Application.Actions;
using MShare.Songs.Api.Messages;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.Queries.V1;

namespace MShare.Songs.Application.Queries.V1.GetByUrl
{
    public class AfterActionHandler : IAfterActionHandler<GetByUrlQuery, GetByUrlResponseDto>
    {
        private readonly IIntegrationBus _integrationBus;

        public AfterActionHandler(IIntegrationBus integrationBus)
            => _integrationBus = integrationBus;

        public async Task Handle(GetByUrlQuery request, GetByUrlResponseDto resonse)
        {
            await _integrationBus.Publish(new SongRequestedEvent() {  CommandId = Guid.NewGuid() });
        }
    }
}