using System;
using MShare.Framework.Types;

namespace MShare.Framework.Infrastructure.AccessToken
{
	public interface IAccessTokenProvider
	{
		Task<Result<string>> GetAsync(bool newToken);
	}
}

