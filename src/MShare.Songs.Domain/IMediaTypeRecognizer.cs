using MShare.Framework.Types;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public interface IMediaTypeRecognizer
	{
        Result<MediaType> From(Uri uri);
    }
}

