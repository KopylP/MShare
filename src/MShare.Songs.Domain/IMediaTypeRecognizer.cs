using MShare.Framework.Types;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public interface IMediaTypeRecognizer
	{
        Result<MediaType> From(Uri uri);
    }

    public static class IMediaTypeRecognizerExtentions
    {
        public static Result<MediaType> From(this IMediaTypeRecognizer recognizer, string uri)
        {
            return recognizer.From(new Uri(uri));
        }
    }
}

