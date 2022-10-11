using System;
using MShare.Framework.Exceptions;

namespace MShare.Framework.Types.Addresses
{
	public class CountryCode3
	{
        public static CountryCode3 Invariant = CountryCode3.Of("Invariant");
        public static CountryCode3 Usa = CountryCode3.Of("USA");

        public string Code { get; init; }

        protected CountryCode3()
        {
        }

        // TODO Add list of valid country codes
        private CountryCode3(string countryCode3)
        {
            Thrower.ThrowIf<ArgumentException>(
                countryCode3 is null || (countryCode3.Length != 3 && countryCode3.ToLowerInvariant() != "invariant"),
                $"{nameof(countryCode3)} is not valid");

            Code = countryCode3.ToUpperInvariant();
        }

        public static CountryCode3 Of(string code) => new(code);

        public override string ToString() => Code;
    }
}

