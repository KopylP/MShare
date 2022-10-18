using System;
using MShare.Framework.Types;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public interface IMetadataExtractor
	{
		public Result<string> ExtractId(string url, StreamingServiceType streamingServiceType, MediaType mediaType);
		public Result<CountryCode2> ExtractRegion(string url, StreamingServiceType streamingServiceType, MediaType mediaType);
    }
}

