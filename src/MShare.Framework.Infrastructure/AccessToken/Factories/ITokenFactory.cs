using System;
namespace MShare.Framework.Infrastructure.AccessToken.Factories
{
	public interface ITokenFactory
	{
		public (string token, int timeToLife) Create(DateTime expiresDate);
	}

	public static class ITokenFactoryExtentions
	{
		public static (string token, int timeToLife) Create(this ITokenFactory factory, int daysOfLife)
		{
			var date = DateTime.UtcNow.AddDays(7);
			return factory.Create(date);
		}
	}
}