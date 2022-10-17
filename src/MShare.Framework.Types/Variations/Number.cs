using System;
namespace MShare.Framework.Types.Variations
{
	public abstract record Number
	{
		public abstract int Value { get; }

        public static Number Of(int value) => new DefaultNumber(value);
        public static Number Random(IntRange? range = default) => new RandomNumber(range);
        public static Number Random(int to) => new RandomNumber(IntRange.Of(to));

        public static implicit operator int(Number number) => number.Value;

        public override string ToString() => Value.ToString();

        private record DefaultNumber : Number
        {
            private readonly int _value;

            public override int Value => _value;

            public DefaultNumber(int number) => _value = number;

            public override string ToString() => Value.ToString();
        }

        private record RandomNumber : Number
        {
            private readonly int _value;

            public RandomNumber(IntRange? range = default) => _value = GetRandomValue(range);

            public override int Value => _value;

            public override string ToString() => Value.ToString();

            private int GetRandomValue(IntRange? range)
            {
                var random = new Random();

                if (range is null)
                    return random.Next();

                return random.Next(range.From, range.To);
            }
        }
    }
}

