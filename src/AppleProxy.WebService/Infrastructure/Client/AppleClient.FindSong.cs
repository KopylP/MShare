using System;
using AppleProxy.WebService.Infrastructure.Client.Models;
using Flurl;
using Flurl.Http;
using Proxy.Api;
using Unidecode.NET;

namespace AppleProxy.WebService.Infrastructure.Client
{
	public partial class AppleClient
	{
        public async Task<SongsResponseDto> FindTrackAsync(FindSongsRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}

