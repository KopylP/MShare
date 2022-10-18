using Flurl;
using Proxy.Api;

namespace AppleProxy.WebService.Helpers
{
	public static class StringExtentions
	{
        public static string GetApplePhotoSizeUrl(this string imageUrl, int newSize)
			=> imageUrl.RemovePathSegment()
					.AppendPathSegment($"{newSize}x{newSize}bb.jpg")
					.ToString();
    }
}