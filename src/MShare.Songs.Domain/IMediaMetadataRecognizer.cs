using MShare.Framework.Types;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public interface IMediaMetadataRecognizer
	{
        Result<(StreamingServiceType ServiceType, MediaType MediaType)> From(Uri uri);
    }
}

