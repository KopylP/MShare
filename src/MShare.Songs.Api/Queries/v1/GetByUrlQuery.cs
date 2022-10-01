using System;
using MShare.Framework.Api;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;

namespace MShare.Songs.Api.Queries.V1
{
	public record GetByUrlQuery : IQuery<GetByUrlResponseDto>
	{
		public string Url { get; set; }

        public static GetByUrlQuery Of(string url)
            => new GetByUrlQuery
            {
                Url = url
            };
    }
}