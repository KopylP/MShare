using System;
using System.Net.Mail;

namespace MShare.Identity.Domain
{
	public record Email
	{
		public virtual string Value { get; protected set; }

        protected Email() { }
		private Email(string email) => Value = email;

		public static Email Parse(string email)
		{
            try
            {
                MailAddress mailAddress = new MailAddress(email);

                return new Email(mailAddress.ToString());
            }
            catch (FormatException)
            {
                throw new ArgumentException("Mail address is invalid");
            }
        }
	}
}

