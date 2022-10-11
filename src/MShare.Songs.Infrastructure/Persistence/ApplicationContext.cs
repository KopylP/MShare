using System;
using Microsoft.EntityFrameworkCore;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Persistence
{
	public class ApplicationContext : DbContext
	{
        public DbSet<AlbumEntity> Albums { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}

