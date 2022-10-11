using System;
using MShare.Framework.Types;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public interface IIdExtractor
	{
		public Result<string> Extract(string url, StreamingServiceType streamingServiceType, MediaType mediaType);
	}
}

