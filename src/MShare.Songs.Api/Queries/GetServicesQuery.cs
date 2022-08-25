﻿using System;
using MShare.Framework.Api;
using MShare.Songs.Api.Queries.Dtos;

namespace MShare.Songs.Api.Queries
{
	public class GetServicesQuery : IQuery<ServicesResponseDto>
	{
        public static GetServicesQuery Instance = new GetServicesQuery();
    }
}

