using AutoMapper;
using MediatR;
using MShare.Framework.Application;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
    internal class QueryHandler : IQueryHandler<GetSongByUrlQuery, SongByUrlResponseDto>
    {
        private readonly IStreamingServiceTypeRecognizer _recognizer;
        private readonly IProxyServiceClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public QueryHandler(IStreamingServiceTypeRecognizer recognizer, IProxyServiceClientFactory clientFactory, IMapper mapper, IMediator mediator)
        {
            _recognizer = recognizer;
            _clientFactory = clientFactory;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<SongByUrlResponseDto> Handle(GetSongByUrlQuery request, CancellationToken cancellationToken)
        {
            var recognizerServiceResult = _recognizer.From(new Uri(request.SongUrl));
            BadRequestException.ThrowIf(recognizerServiceResult.IsFail, "Can not recognize url service");

            var serviceClient = _clientFactory.Create(recognizerServiceResult.Data);

            var songResult = await serviceClient.GetSongByUrlAsync(request.SongUrl);
            NotFoundException.ThrowIf(songResult is null, "Song by url not found");

            var services = await _mediator.Send(GetServicesQuery.Instance);

            return _mapper.Map<SongByUrlResponseDto>((songResult, services));
        }
    }
}