using System;
using MShare.Framework.Exceptions;

namespace MShare.Framework.Types
{
    public record Isrc
    {
        public string Value { get; protected set; }

        protected Isrc()
        {
        }

        private Isrc(string? isrc)
        {
            Thrower.ThrowIf<ArgumentException>(string.IsNullOrWhiteSpace(isrc), "Isrc is null or empty");
            Thrower.ThrowIf<ArgumentException>(isrc.Length is not 12, "Isrc code has incorrect length");

            Value = isrc;
        }

        public static Isrc Of(string? isrc) => new Isrc(isrc);

        public static implicit operator Isrc(string? isrc) => Of(isrc);
    }
}

