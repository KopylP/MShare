using System;
using MediatR;
using MShare.Framework.Application;
using MShare.Framework.Domain;
using MShare.Framework.Types.Addresses;
using MShare.Framework.Types.Variations;
using MShare.Songs.Api.Commands.V1;
using MShare.Songs.Domain;
using MShare.Songs.Domain.Specifications;

namespace MShare.Songs.Application.Commands.SaveSong
{
	public class CommandHandler : ICommandHandler<SaveSongCommand>
	{
        private readonly IUnitOfWorkProvider _provider;
        private readonly ISongRepository _songRepository;

        public CommandHandler(IUnitOfWorkProvider provider, ISongRepository songRepository)
        {
            _provider = provider;
            _songRepository = songRepository;
        }

        public async Task<Unit> Handle(SaveSongCommand request, CancellationToken cancellationToken)
        {
            using var uow = _provider.Create();

            var song = await _songRepository.FirstOrDefaultAsync(SongByIdSpecification.Of(request.SourceId, request.ServiceType, request.Region));

            if (song is null)
            {
                song = new SongEntity(request.SourceId, request.ServiceType, request.Region, request.Isrc ?? "111111111" + Number.Random((100, 999))) // TODO Remove mock data 
                {
                    Name = request.Name,
                    ArtistName = request.ArtistName,
                    SourceUrl = request.SourceUrl,
                    ImageThumbnailUrl = request.ImageThumbnailUrl,
                    ImageUrl = request.ImageUrl,
                    AlbumSourceId = request.AlbumSourceId,
                    AlbumName = request.AlbumName
                };

                await _songRepository.SaveAsync(song);

                await uow.CommitAsync();
            }

            return Unit.Value;
        }
    }
}

