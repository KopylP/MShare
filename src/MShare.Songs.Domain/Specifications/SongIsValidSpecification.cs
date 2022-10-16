using System;
using System.Linq.Expressions;
using MShare.Framework.Domain;

namespace MShare.Songs.Domain.Specifications
{
	public class SongIsValidSpecification : SpecificationBase<SongEntity>
	{
        public static SongIsValidSpecification Instance => new SongIsValidSpecification();

        public override Expression<Func<SongEntity, bool>> Criteria => p => IsValid(p);

        private static bool IsValid(SongEntity song) =>
            !string.IsNullOrEmpty(song.Name)
            && !string.IsNullOrEmpty(song.ArtistName)
            && !string.IsNullOrEmpty(song.SourceUrl)
            && !string.IsNullOrEmpty(song.ImageUrl)
            && !string.IsNullOrEmpty(song.ImageThumbnailUrl)
            && !string.IsNullOrEmpty(song.AlbumName)
            && !string.IsNullOrEmpty(song.AlbumSourceId)
            && song.ServiceType != Abstractions.StreamingServiceType.None;
    }
}