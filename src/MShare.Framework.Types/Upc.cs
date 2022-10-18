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
			Thrower.ThrowIf<ArgumentException>(upc.Length > 20, "Upc code has incorrect length");

            Value = upc;
        }

		public static Upc Of(string? upc) => new Upc(upc);

        public static implicit operator Upc(string? upc) => Of(upc);
    }
}

