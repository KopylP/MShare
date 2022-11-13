using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MShare.Framework.Infrastructure.AccessToken.Factories.Apple
{
	public class TokenFactory : ITokenFactory
	{
		private readonly string _secret;
		private readonly string _keyId;
		private readonly string _issuer;

		public TokenFactory(IConfiguration configuration)
		{
			_secret = configuration["Apple:Token:Secret"];
			_keyId = configuration["Apple:Token:KeyId"];
			_issuer = configuration["Apple:Token:Issuer"];
		}

        public (string token, int timeToLife) Create(DateTime expiresDate)
        {
            var secret = _secret;
            var a = GetEllipticCurveAlgorithm(secret);
            var securityKey = new ECDsaSecurityKey(a);
            securityKey.KeyId = _keyId;
            var issuer = _issuer;

            var signingCredentials = new SigningCredentials(securityKey, "ES256");
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expiresDate,
                Issuer = issuer,
                SigningCredentials = signingCredentials,
                IssuedAt = now,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), (int)(expiresDate - now).TotalSeconds);
        }

        private ECDsa GetEllipticCurveAlgorithm(string privateKey)
        {
            var result = ECDsa.Create();
            result.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
            return result;
        }
    }
}

