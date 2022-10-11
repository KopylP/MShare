using System;
using System.Linq.Expressions;
using MShare.Framework.Domain;

namespace MShare.Songs.Domain.Specifications
{
    public class AlbumIsValidSpecification : SpecificationBase<AlbumEntity>
    {
        public static AlbumIsValidSpecification Instance => new AlbumIsValidSpecification();

        public override Expression<Func<AlbumEntity, bool>> Criteria => p => IsValid(p);

        private static bool IsValid(AlbumEntity album) =>
            !string.IsNullOrEmpty(album.Name)
            && !string.IsNullOrEmpty(album.ArtistName)
            && !string.IsNullOrEmpty(album.SourceUrl)
            && !string.IsNullOrEmpty(album.ImageUrl)
            && !string.IsNullOrEmpty(album.ImageThumbnailUrl);
    }
}

