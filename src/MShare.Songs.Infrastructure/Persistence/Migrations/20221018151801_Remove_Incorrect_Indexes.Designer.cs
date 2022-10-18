﻿// <auto-generated />
using System;
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
    [Migration("20221018151801_Remove_Incorrect_Indexes")]
    partial class Remove_Incorrect_Indexes
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
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("artist_name");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date")
                        .HasDefaultValueSql("NOW()");

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

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("service_type");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("source_id");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("source_url");

                    b.HasKey("Id")
                        .HasName("PK_Album");

                    b.ToTable("album", (string)null);
                });

            modelBuilder.Entity("MShare.Songs.Domain.SongEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

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

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date")
                        .HasDefaultValueSql("NOW()");

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

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("service_type");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("source_id");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("source_url");

                    b.HasKey("Id")
                        .HasName("PK_Song");

                    b.ToTable("song", (string)null);
                });

            modelBuilder.Entity("MShare.Songs.Domain.AlbumEntity", b =>
                {
                    b.OwnsOne("MShare.Framework.Types.Upc", "Upc", b1 =>
                        {
                            b1.Property<long>("AlbumEntityId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("upc");

                            b1.HasKey("AlbumEntityId");

                            b1.ToTable("album");

                            b1.WithOwner()
                                .HasForeignKey("AlbumEntityId");
                        });

                    b.OwnsOne("MShare.Framework.Types.Addresses.CountryCode2", "Region", b1 =>
                        {
                            b1.Property<long>("AlbumEntityId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("varchar(10)")
                                .HasColumnName("region");

                            b1.HasKey("AlbumEntityId");

                            b1.ToTable("album");

                            b1.WithOwner()
                                .HasForeignKey("AlbumEntityId");
                        });

                    b.Navigation("Region")
                        .IsRequired();

                    b.Navigation("Upc")
                        .IsRequired();
                });

            modelBuilder.Entity("MShare.Songs.Domain.SongEntity", b =>
                {
                    b.OwnsOne("MShare.Framework.Types.Isrc", "Isrc", b1 =>
                        {
                            b1.Property<long>("SongEntityId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("isrc");

                            b1.HasKey("SongEntityId");

                            b1.ToTable("song");

                            b1.WithOwner()
                                .HasForeignKey("SongEntityId");
                        });

                    b.OwnsOne("MShare.Framework.Types.Addresses.CountryCode2", "Region", b1 =>
                        {
                            b1.Property<long>("SongEntityId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("varchar(10)")
                                .HasColumnName("region");

                            b1.HasKey("SongEntityId");

                            b1.ToTable("song");

                            b1.WithOwner()
                                .HasForeignKey("SongEntityId");
                        });

                    b.Navigation("Isrc")
                        .IsRequired();

                    b.Navigation("Region")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
