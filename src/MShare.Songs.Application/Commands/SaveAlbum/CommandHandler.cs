using System;
using MediatR;
using MShare.Framework.Application;
using MShare.Framework.Domain;
using MShare.Framework.Types.Addresses;
using MShare.Framework.Types.Variations;
using MShare.Songs.Api.Commands.V1;
using MShare.Songs.Domain;
using MShare.Songs.Domain.Specifications;
using Polly;

namespace MShare.Songs.Application.Commands.SaveAlbum
{
	public class CommandHandler : ICommandHandler<SaveAlbumCommand>
	{
        private readonly IUnitOfWorkProvider _provider;
        private readonly IAlbumRepository _albumRepository;

        public CommandHandler(IUnitOfWorkProvider provider, IAlbumRepository albumRepository)
        {
            _provider = provider;
            _albumRepository = albumRepository;
        }

        public async Task<Unit> Handle(SaveAlbumCommand request, CancellationToken cancellationToken)
        {
            using var uow = _provider.Create();

            var album = await _albumRepository.FirstOrDefaultAsync(AlbumByIdSpecification.Of(request.SourceId, request.ServiceType, request.Region));

            if (album is null)
            {
                album = new AlbumEntity(request.SourceId, request.ServiceType, request.Region, request.Upc ?? "111111111" + Number.Random((100, 999))) // TODO remove mock data
                {
                    Name = request.Name,
                    ArtistName = request.ArtistName,
                    SourceUrl = request.SourceUrl,
                    ImageThumbnailUrl = request.ImageThumbnailUrl,
                    ImageUrl = request.ImageUrl
                };

                await _albumRepository.SaveAsync(album);

                await uow.CommitAsync();
            }

            return Unit.Value;
        }
    }
}

