using System;
using MShare.Framework.Types;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public interface IStreamingServiceTypeRecognizer
	{
		Result<StreamingServiceType> From(Uri uri);
	}

	public static class IStreamingServiceTypeRecognizerExtentions
	{
		public static Result<StreamingServiceType> From(this IStreamingServiceTypeRecognizer recognizer, string uri)
		{
			return recognizer.From(new Uri(uri));
		}
    }
}