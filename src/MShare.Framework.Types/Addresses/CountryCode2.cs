﻿using System;
using MShare.Framework.Exceptions;

namespace MShare.Framework.Types.Addresses
{
	public record CountryCode2
	{
		public static CountryCode2 Invariant => CountryCode2.Of("Invariant");
		public static CountryCode2 Us => CountryCode2.Of("US");

        public string Code { get; init; }

		protected CountryCode2()
		{
		}

		// TODO Add list of valid country codes
		private CountryCode2(string countryCode2)
		{
			Thrower.ThrowIf<ArgumentException>(
				countryCode2 is null || (countryCode2.Length != 2 && countryCode2.ToLowerInvariant() != "invariant"),
				$" Country code {nameof(countryCode2)} is not valid");

			Code = countryCode2.ToUpperInvariant();
        }

		public static CountryCode2 Of(string code) => new(code);

		public override string ToString() => Code;

		public static implicit operator CountryCode2(string code) => CountryCode2.Of(code);
		public static implicit operator string(CountryCode2 code) => code.ToString();
    }
}

