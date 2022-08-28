﻿using System;
using System.Linq.Expressions;
using MShare.Framework.Domain;

namespace MShare.Songs.Domain.Specifications
{
	public partial class ValidSongUrlSpecification
    {
        private class SpotifyUrlSpecification : BaseSpecification<Uri>
        {
            public static SpotifyUrlSpecification Instance => new SpotifyUrlSpecification();

            public override Expression<Func<Uri, bool>> Criteria
                => p => p.Host.ToLower() == "open.spotify.com"
                    && p.PathAndQuery.ToLower().Contains("track");
        }
    }
}
