using System;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Api.Exceptions;
using MShare.Framework.Infrastructure.AccessToken;
using MShare.Framework.Types;
using MShare.Framework.Types.Variations;
using Polly;

namespace Flurl.Http
{
    public static class IFlurlRequestExtentions
    {
        public static IFlurlRequest ToRequest(this Url url)
            => new FlurlRequest(url);

        public static IFlurlRequest ToRequest(this string url)
            => new FlurlRequest(new Url(url));

        public static Url SetQueryParamIf(this Url request, bool condition, string param, object? obj)
        {
            if (condition)
            {
                request.SetQueryParam(param, obj);
            }

            return request;
        }

        public static Url AddParamIfNotNull<T>(this Url request, T? value, string param, object obj) where T : struct
            => request.SetQueryParamIf(value.HasValue, param, obj);

        public static Url AddParamIfNotNullOrWhiteSpace(this Url request, string? value, string param, object obj)
            => request.SetQueryParamIf(!string.IsNullOrWhiteSpace(value), param, obj);

        public static IFlurlRequest WithBearerToken(this IFlurlRequest request, string? bearerToken)
            => request.WithHeader("Authorization", $"Bearer {bearerToken}");

        public static IFlurlRequest WithBasicToken(this IFlurlRequest request, string? basicToken)
            => request.WithHeader("Authorization", $"Basic {basicToken}");

        public async static Task<TResponse> GetJsonWithRetryAsync<TResponse>(this Url url, (int RetryCount, IntRange WaitRangeInMilliseconds) retryPolicy)
        {
            var timeSpans = Enumerable.Range(0, retryPolicy.RetryCount)
                .Select(p => TimeSpan.FromMilliseconds(Number.Random(retryPolicy.WaitRangeInMilliseconds)));

            return await Policy.Handle<FlurlHttpException>()
                .WaitAndRetryAsync(timeSpans)
                .ExecuteAsync<TResponse>(async () => await url.GetJsonAsync<TResponse>());
        }

        public static Url AppendPathSegmentIf(this Url request, bool condition, string value)
        {
            if (condition)
                return request.AppendPathSegment(value);

            return request;
        }

        public static Url AppendPathSegmentIf(this string request, bool condition, string value)
        {
            var url = new Url(request);
            return url.AppendPathSegmentIf(condition, value);
        }

        public static async Task<T> GetAuthorizedAsync<T>(this Flurl.Url url, IAccessTokenProvider tokenProvider)
        {
            var currentToken = await tokenProvider.GetAsync(newToken: false);

            IFlurlResponse? response = null;
            var request = url.ToRequest();

            if (currentToken.IsSuccess)
            {
                request = request.WithBearerToken(currentToken.Data);
                response = await request.AllowAnyHttpStatus().GetAsync();
            }

            if (currentToken.IsFail || response?.StatusCode != StatusCodes.Status200OK)
            {
                var newToken = await tokenProvider.GetAsync(newToken: true);

                if (newToken.IsFail)
                    throw new UnauthorizedException();

                request = request.WithBearerToken(newToken.Data);
            }

            response = await request.AllowAnyHttpStatus().GetAsync();

            if (response.StatusCode != StatusCodes.Status200OK)
                throw new ApiException(response.StatusCode);

            return await response.GetJsonAsync<T>();
        }
    }
}


