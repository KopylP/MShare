using System;
using MShare.Framework.Types;

namespace MShare.Framework.Infrastructure.AccessToken;

public interface IAccessTokenStore
{
    public Result<string> Get();
    public void Set(string? accessToken, int expiresIn);
}

