using System;
using MShare.Framework.Exceptions;

namespace MShare.UserProfiles.Domain
{
	public record UserId
	{
		public string Id { get; protected set; }

		protected UserId()
		{
		}

		private UserId(string userId)
		{
			Id = userId;
		}

		public static UserId Of(string userId)
		{
            Thrower.ThrowIf<AggregateException>(string.IsNullOrWhiteSpace(userId), "User id should be provided");
			return new UserId(userId);
        }

		public override string ToString() => Id;
    }
}

