using System;
using Flurl.Http;
using MShare.Framework.Infrastructure.AccessToken;
using MShare.Framework.Types;

namespace SpotifyProxy.WebService.Infrastructure.Client
{
	internal class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly IAccessTokenStore _accessTokenStore;
        private readonly string _authUrl;
        private readonly string _authCredentials;

        public AccessTokenProvider(IAccessTokenStore accessTokenStore, IConfiguration configuration)
		{
            _accessTokenStore = accessTokenStore;

            _authUrl = configuration.GetValue<string>("SpotifyAuthUrl");
            _authCredentials = configuration.GetValue<string>("SpotifyAuthCredentials");
        }

        public async Task<Result<string>> GetAsync(bool newToken)
        {
            if (!newToken)
                return _accessTokenStore.Get();

            var response = await GetNewTokenAsync();

            if (response.IsSuccess)
            {
                _accessTokenStore.Set(response.Data?.access_token, response.Data?.expires_in ?? 0);
                return Result<string>.Success(response.Data?.access_token);
            }

            return Result<string>.Fail(response.FailMessage);
        }


        private async Task<HttpResult<TokenResponse>> GetNewTokenAsync()
        {
            var request = _authUrl.ToRequest().WithBasicToken(_authCredentials);

            try
            {
                var response = await request
                    .PostUrlEncodedAsync(new
                    {
                        grant_type = "client_credentials"
                    });

                var token = await response.GetJsonAsync<TokenResponse>();

                return HttpResult<TokenResponse>.Ok(token);
            }
            catch (FlurlHttpException ex)
            {
                return HttpResult<TokenResponse>.FromStatusCode(ex.StatusCode ?? 500, ex.Message);
            }
        }

        private class TokenResponse
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }
    }
}

