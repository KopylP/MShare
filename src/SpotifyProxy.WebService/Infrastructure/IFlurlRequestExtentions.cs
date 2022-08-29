using Flurl.Http;
using MShare.Framework.Infrastructure.AccessToken;
using MShare.Framework.Types;
using MShare.Framework.WebApi.Exceptions;

namespace SpotifyProxy.WebService.Infrastructure
{
    internal static class IFlurlRequestExtentions
    {
        public static async Task<T> GetAuthorizedAsync<T>(this Flurl.Url url, IServiceProvider provider)
        {
            var accessTokenStore = provider.GetRequiredService<IAccessTokenStore>();
            var configuration = provider.GetRequiredService<IConfiguration>();
            var authUrl = configuration.GetValue<string>("SpotifyAuthUrl");
            var authCredentials = configuration.GetValue<string>("SpotifyAuthCredentials");
            var authRequest = CreateAuthRequest(authUrl, authCredentials);

            var tokenResult = accessTokenStore.Get();

            IFlurlResponse? response = null;
            var request = url.ToRequest();

            if (tokenResult.IsSuccess)
            {
                request.WithBearerToken(tokenResult.Data);
                response = await request.AllowAnyHttpStatus().GetAsync();
            }

            if (tokenResult.IsFail || response?.StatusCode != StatusCodes.Status200OK)
            {
                var result = await request.InterсeptNewBearerAsync(authRequest);

                if (result.IsFail)
                    throw new UnauthorizedException();

                accessTokenStore.Set(result.Data?.access_token, result.Data?.expires_in ?? default);
            }

            response = await request.AllowAnyHttpStatus().GetAsync();

            if (response.StatusCode != StatusCodes.Status200OK)
                throw new ApiException(response.StatusCode);

            return await response.GetJsonAsync<T>();
        }

        private static IFlurlRequest CreateAuthRequest(string authUrl, string authCredentials)
            => authUrl.ToRequest().WithBasicToken(authCredentials);


        private static async Task<Result<TokenResponse>> InterсeptNewBearerAsync(
            this IFlurlRequest request,
            IFlurlRequest authRequest)
        {
            var authResult = await AuthAsync(authRequest);

            if (authResult.IsSuccess)
            {
                request.WithBearerToken(authResult.Data?.access_token ?? "");
            }

            return authResult;
        }

        private static async Task<HttpResult<TokenResponse>> AuthAsync(IFlurlRequest authRequest)
        {
            try
            {
                var response = await authRequest
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

