using System;
namespace MShare.Framework.Types
{
	public record IntRange
	{
		public int From { get; private init; }
		public int To { get; private init; }

		private IntRange(int from, int to)
		{
			if (from > to)
				throw new ArgumentException("'From' should be equal or less then 'to'");

			From = from;
			To = to;
		}

		public static IntRange Of(int from, int to) => new IntRange(from, to);
		public static IntRange Of(int to) => new IntRange(0, to);
    }
}

