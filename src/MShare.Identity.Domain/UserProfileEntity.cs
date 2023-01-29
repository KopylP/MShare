using System;
using MShare.Framework.Exceptions;

namespace MShare.Identity.Domain
{
	public class UserProfileEntity
	{
        public UserId Id { get; protected set; }
        public FullName FullName { get; protected set; }
        public Email Email { get; protected set; }

        public UserProfileEntity(UserId id, FullName fullName, Email email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }

        public static UserProfileEntity Of(UserId id, FullName fullName, Email email)
        {
            Thrower.ThrowIf<ArgumentException>(id is null, "User id cannot be null");
            Thrower.ThrowIf<ArgumentException>(fullName is null, "Full name cannot be null");
            Thrower.ThrowIf<ArgumentException>(email is null, "Email cannot be null");

            return new UserProfileEntity(id, fullName, email);
        }
    }
}

