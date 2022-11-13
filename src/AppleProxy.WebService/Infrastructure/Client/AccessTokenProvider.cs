using System;
using MShare.Framework.Infrastructure.AccessToken;
using MShare.Framework.Infrastructure.AccessToken.Factories;
using MShare.Framework.Types;

namespace AppleProxy.WebService.Infrastructure.Client
{
	internal class AccessTokenProvider : IAccessTokenProvider
	{
        private readonly ITokenFactory _tokenFactory;
        private readonly IAccessTokenStore _accessTokenStore;

		public AccessTokenProvider(ITokenFactory tokenFactory, IAccessTokenStore accessTokenStore)
		{
            _tokenFactory = tokenFactory;
            _accessTokenStore = accessTokenStore;
		}

        public async Task<Result<string>> GetAsync(bool newToken)
        {
            if (!newToken)
                return _accessTokenStore.Get();

            var token = _tokenFactory.Create(daysOfLife: 1);
            _accessTokenStore.Set(token.token, token.timeToLife);

            return await Task.FromResult(Result<string>.Success(token.token));
        }
    }
}