using System;
using MShare.Framework.Types;

namespace MShare.Songs.Domain
{
	public interface IStreamingServiceTypeRecognizer
	{
		Result<StreamingServiceType> From(Uri uri);
	}
}

