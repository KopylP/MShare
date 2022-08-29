using System;
using MShare.Framework.Types;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public interface IStreamingServiceTypeRecognizer
	{
		Result<StreamingServiceType> From(Uri uri);
	}
}