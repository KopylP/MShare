﻿// <auto-generated />
using MShare.Songs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221014142817_Add_SongEntity")]
    partial class Add_SongEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MShare.Songs.Domain.AlbumEntity", b =>
                {
                    b.Property<string>("SourceId")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("source_id");

                    b.Property<string>("ServiceType")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("service_type");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("artist_name");

                    b.Property<string>("ImageThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("image_thumbnail_url");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("name");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("source_url");

                    b.HasKey("SourceId", "ServiceType")
                        .HasName("PK_Album");

                    b.ToTable("album", (string)null);
                });

            modelBuilder.Entity("MShare.Songs.Domain.SongEntity", b =>
                {
                    b.Property<string>("SourceId")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("source_id");

                    b.Property<string>("ServiceType")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("service_type");

                    b.Property<string>("AlbumName")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("album_name");

                    b.Property<string>("AlbumSourceId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("album_source_id");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("artist_name");

                    b.Property<string>("ImageThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("image_thumbnail_url");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("name");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("source_url");

                    b.HasKey("SourceId", "ServiceType")
                        .HasName("PK_Song");

                    b.ToTable("song", (string)null);
                });

            modelBuilder.Entity("MShare.Songs.Domain.AlbumEntity", b =>
                {
                    b.OwnsOne("MShare.Framework.Types.Addresses.CountryCode3", "Country", b1 =>
                        {
                            b1.Property<string>("AlbumEntitySourceId")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("AlbumEntityServiceType")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("varchar(10)")
                                .HasColumnName("country");

                            b1.HasKey("AlbumEntitySourceId", "AlbumEntityServiceType");

                            b1.ToTable("album");

                            b1.WithOwner()
                                .HasForeignKey("AlbumEntitySourceId", "AlbumEntityServiceType");
                        });

                    b.Navigation("Country")
                        .IsRequired();
                });

            modelBuilder.Entity("MShare.Songs.Domain.SongEntity", b =>
                {
                    b.OwnsOne("MShare.Framework.Types.Addresses.CountryCode3", "Country", b1 =>
                        {
                            b1.Property<string>("SongEntitySourceId")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("SongEntityServiceType")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("varchar(10)")
                                .HasColumnName("country");

                            b1.HasKey("SongEntitySourceId", "SongEntityServiceType");

                            b1.ToTable("song");

                            b1.WithOwner()
                                .HasForeignKey("SongEntitySourceId", "SongEntityServiceType");
                        });

                    b.Navigation("Country")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
