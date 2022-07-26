using System;
using Flurl;
using Flurl.Http;

namespace MShare.Framework.Infrastructure.Clients
{
    public static class IFlurlRequestExtentions
    {
        public static IFlurlRequest ToRequest(this Url url)
            => new FlurlRequest(url);

        public static IFlurlRequest ToRequest(this string url)
            => new FlurlRequest(new Url(url));

        public static Url AddParamIf(this Url request, bool condition, string param, object obj)
        {
            if (condition)
            {
                request.SetQueryParam(param, obj);
            }

            return request;
        }

        public static Url AddParamIfNotNull<T>(this Url request, T? value, string param, object obj) where T : struct
            => request.AddParamIf(value.HasValue, param, obj);

        public static Url AddParamIfNotNullOrWhiteSpace(this Url request, string? value, string param, object obj)
            => request.AddParamIf(!string.IsNullOrWhiteSpace(value), param, obj);

        public static IFlurlRequest WithBearerToken(this IFlurlRequest request, string? bearerToken)
            => request.WithHeader("Authorization", $"Bearer {bearerToken}");

        public static IFlurlRequest WithBasicToken(this IFlurlRequest request, string? basicToken)
            => request.WithHeader("Authorization", $"Basic {basicToken}");
    }
}

