﻿using System;
using Flurl;
using Flurl.Http;
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

        public async static Task<TResponse> GetJsonWithRetryAsync<TResponse>(this Url url, int retryCount, int waitInMilliseconds)
        {
            var timeSpans = Enumerable.Range(0, retryCount).Select(p => TimeSpan.FromMilliseconds(waitInMilliseconds));

            return await Policy.Handle<FlurlHttpException>()
                .WaitAndRetryAsync(timeSpans)
                .ExecuteAsync<TResponse>(async () => await url.GetJsonAsync<TResponse>());
        }

        public async static Task<TResponse> GetJsonWithRetryAsync<TResponse>(this Url url, (int RetryCount, int WaitInMilliseconds) retryPolicy)
            => await url.GetJsonWithRetryAsync<TResponse>(retryPolicy.RetryCount, retryPolicy.WaitInMilliseconds);
    }
}

