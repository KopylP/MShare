using System;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.Infrastructure.Persistance.EntityFramework;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Persistence.Repositories
{
	public class SongRepository : RepositoryBase<SongEntity>, ISongRepository
    {
        public SongRepository(DbContext context) : base(context)
        {
        }
    }
}

