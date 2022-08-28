using System;
using System.Linq.Expressions;
using MShare.Framework.Domain;

namespace MShare.Songs.Domain.Specifications
{
	public partial class ValidSongUrlSpecification: BaseSpecification<Uri>
    {
        private class AppleUrlSpecification : BaseSpecification<Uri>
        {
            public static AppleUrlSpecification Instance => new AppleUrlSpecification();

            public override Expression<Func<Uri, bool>> Criteria
                => p => p.Host.ToLower() == "music.apple.com"
                    && p.PathAndQuery.ToLower().Contains("album")
                    && p.PathAndQuery.ToLower().Contains("i");
        }
    }
}

