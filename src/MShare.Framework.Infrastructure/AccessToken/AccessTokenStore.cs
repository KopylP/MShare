using System;
using Microsoft.Extensions.Caching.Memory;
using MShare.Framework.Types;

namespace MShare.Framework.Infrastructure.AccessToken;

internal class AccessTokenStore : IAccessTokenStore
{
    private readonly IMemoryCache _cache;
    private const string KEY = nameof(AccessTokenStore) + "_" + "ACCESS_TOKEN_KEY"; 

    public AccessTokenStore(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Result<string> Get()
    {
        _cache.TryGetValue(KEY, out string token);

        if (string.IsNullOrWhiteSpace(token))
        {
            return Result<string>.Fail();
        }

        return Result<string>.Success(token);
    }

    public void Set(string? token, int expiresIn)
    {
        _cache.Set(KEY, token, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiresIn - 1)
        });
    }
}

