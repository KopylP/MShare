namespace System
{
	public static class StringExtentions
	{
		private static string[] _featStarts = new[] { "ft.", "feat.", "(ft ", "(feat ", "(ft.", "(feat." };

		private static string RemoveFeatsFromName(this string songName)
		{
			int i = 0;
			int index = -1;

			while (i < _featStarts.Length && index < 0)
			{
				index = songName.IndexOf(_featStarts[i++], StringComparison.InvariantCultureIgnoreCase);
			}

			if (index > 0)
				return songName.Substring(0, index - 1);

			return songName;
		}

		public static string PrepareSongName(this string songName)
			=> songName
				.RemoveFeatsFromName()
				.Trim();
	}
}