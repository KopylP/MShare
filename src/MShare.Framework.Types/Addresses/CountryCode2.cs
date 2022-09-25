using System;
using MShare.Framework.Exceptions;

namespace MShare.Framework.Types.Addresses
{
	public record CountryCode2
	{
		public string Code { get; init; }

		// TODO Add list of valid country codes
		private CountryCode2(string countryCode2)
		{
			Thrower.ThrowIf<ArgumentException>(
				countryCode2 is null || countryCode2.Length != 2,
				$"{nameof(countryCode2)} is not valid");

			Code = countryCode2.ToUpperInvariant();
        }

		public static CountryCode2 Of(string code) => new(code);

		public override string ToString() => Code;
    }
}

