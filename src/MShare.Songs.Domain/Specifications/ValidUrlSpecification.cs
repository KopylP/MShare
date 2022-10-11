using System;
using System.Linq.Expressions;
using MShare.Framework.Domain;

namespace MShare.Songs.Domain.Specifications
{
    public class ValidUrlSpecification  : SpecificationBase<Uri>
    {
        private string[] _allowedHosts = new string[]
        {
            "music.apple.com",
            "open.spotify.com",
            "music.youtube.com"
        };

        public static ValidUrlSpecification Instance => new ValidUrlSpecification();

        public override Expression<Func<Uri, bool>> Criteria =>
            p => _allowedHosts.Contains(p.Host.ToLower());
    }
}