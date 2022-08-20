using Flurl;

namespace AppleProxy.WebService.Helpers
{
	public static class StringExtentions
	{
		public static string GetAppleSongId(this string urlStr)
		{
			var url = new Uri(urlStr);
            string queryString = url.Query;
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);
			return queryDictionary?.Get("i") ?? "";
        }

		public static string GetApplePhotoSizeUrl(this string imageUrl, int newSize)
			=> imageUrl.RemovePathSegment()
					.AppendPathSegment($"{newSize}x{newSize}bb.jpg")
					.ToString();
    }
}