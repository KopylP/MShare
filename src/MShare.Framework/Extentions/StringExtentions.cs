using System;
namespace System
{
	public static class StringExtentions
	{
		public static string RemoveFrom(this string str, char character)
		{
			var index = str.IndexOf(character);

			if (index <= 0)
				return str;

			return str.Substring(0, index);
		}
	}
}

