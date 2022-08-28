using System;
using MShare.Framework.Api;
using MShare.Songs.Api.V1.Queries.Dtos;

namespace MShare.Songs.Api.V1.Queries
{
	public class GetServicesQuery : IQuery<ServicesResponseDto>
	{
        public static GetServicesQuery Instance = new GetServicesQuery();
    }
}

