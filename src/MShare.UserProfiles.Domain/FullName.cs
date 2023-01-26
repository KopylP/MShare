using System;
using MShare.Framework.Exceptions;

namespace MShare.UserProfiles.Domain
{
	public record FullName
	{
		public virtual string FirstName { get; protected set; }
		public virtual string LastName { get; protected set; }

		protected FullName() { }
		public FullName(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		public static FullName Of(string firstName, string lastName)
		{
			Thrower.ThrowIf<ArgumentException>(string.IsNullOrWhiteSpace(firstName), "First name should be provided");

            return new FullName(firstName, lastName);
        }

        public override string ToString() => $"{FirstName} {LastName}".Trim();
    }
}

