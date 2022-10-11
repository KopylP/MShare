using System;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.Infrastructure.Persistance.EntityFramework;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Persistence.Repositories
{
    public class AlbumRepository : RepositoryBase<AlbumEntity, ApplicationContext>, IAlbumRepository
    {
        public AlbumRepository(DbContext context) : base(context)
        {
        }
    }
}

