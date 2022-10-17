using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Persistence
{
	public class AlbumTypeConfiguration : IEntityTypeConfiguration<AlbumEntity>
	{
        public void Configure(EntityTypeBuilder<AlbumEntity> builder)
        {
            builder.ToTable("album");

            //builder.HasKey(p => new { p.SourceId, p.ServiceType, p.Country.Code })
            builder.HasKey(p => new
            {
                p.SourceId,
                p.ServiceType
            })
            .HasName("PK_Album");

            builder
                .Property(p => p.ServiceType)
                .HasConversion<string>()
                .HasColumnName("service_type")
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.ArtistName) 
                .IsRequired()
                .HasColumnType("varchar(1000)")
                .HasColumnName("artist_name");

            builder.OwnsOne(p => p.Region)
                .Property(p => p.Code)
                .HasColumnType("varchar(10)")
                .HasColumnName("region");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(500)")
                .HasColumnName("name");

            builder.Property(p => p.SourceId)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasColumnName("source_id");

            builder.Property(p => p.SourceUrl)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("source_url");

            builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasColumnType("varchar(300)")
                .HasColumnName("image_url");

            builder.Property(p => p.ImageThumbnailUrl)
                .IsRequired()
                .HasColumnType("varchar(300)")
                .HasColumnName("image_thumbnail_url");

            builder.Property(p => p.CreationDate)
                .IsRequired()
                .HasColumnName("timestamp")
                .HasColumnName("creation_date")
                .HasDefaultValueSql("NOW()");

            builder.OwnsOne(p => p.Upc)
                .Property(p => p.Value)
                .IsRequired()
                .HasColumnName("varchar(15)")
                .HasColumnName("upc");

            builder.OwnsOne(x => x.Upc)
                .HasIndex(x => x.Value)
                .HasDatabaseName("IDX_Upc_Unique")
                .IsUnique();
        }
    }
}