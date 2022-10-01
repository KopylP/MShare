using System;
using MShare.Framework.Api;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Api.V1.Queries
{
	public record GetServicesQuery : IQuery<ServicesResponseDto>
	{
        public static GetServicesQuery Instance = new GetServicesQuery();
    }
}

