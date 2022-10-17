using System;
using MShare.Framework.Exceptions;

namespace MShare.Framework.Types
{
	public record Upc
	{
		public string Value { get; protected set; }

		protected Upc()
		{
		}

		private Upc(string? upc)
		{
            Thrower.ThrowIf<ArgumentException>(string.IsNullOrWhiteSpace(upc), "Upc is null or empty");

            var value = long.Parse(upc).ToString();

			Thrower.ThrowIf<ArgumentException>(value.Length < 12 || value.Length > 13, "Upc code has incorrect length");

            Value = value;
        }

		public static Upc Of(string? upc) => new Upc(upc);

        public static implicit operator Upc(string? upc) => Of(upc);
    }
}

