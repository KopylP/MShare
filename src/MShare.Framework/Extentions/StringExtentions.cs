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

		public static string GetSpotifyId(this string url)
		{
            return url.Replace("//", "/")
				.Split("/")[3]
				.RemoveFrom('?');
        }

        public static string GetAppleSongId(this string urlStr)
        {
            var url = new Uri(urlStr);
            string queryString = url.Query;
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);

            return queryDictionary?.Get("i") ?? "";
        }

        public static string GetAppleCollectionId(this string urlStr)
        {
            var url = new Uri(urlStr.RemoveFrom('?'));
            string id = url.PathAndQuery.Split("/").Last();

            return id;
        }
    }
}

