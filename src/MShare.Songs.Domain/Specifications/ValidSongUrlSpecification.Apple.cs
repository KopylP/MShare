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
                => p => p.Host.ToLower() == "itunes.apple.com"
                    && p.PathAndQuery.ToLower().Contains("lookup")
                    && p.PathAndQuery.ToLower().Contains("id");
        }
    }
}

