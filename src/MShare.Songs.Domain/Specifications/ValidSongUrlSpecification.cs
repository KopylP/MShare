using System;
using System.Linq.Expressions;
using MShare.Framework.Domain;

namespace MShare.Songs.Domain.Specifications
{
    public partial class ValidSongUrlSpecification : BaseSpecification<Uri>
    {
        public static ValidSongUrlSpecification Instance => new ValidSongUrlSpecification();

        public override Expression<Func<Uri, bool>> Criteria =>
            Nothing
                .Or(SpotifyUrlSpecification.Instance)
                .Or(AppleUrlSpecification.Instance)
                .Criteria;
    }
}

