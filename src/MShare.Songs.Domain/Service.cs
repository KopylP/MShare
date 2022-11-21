using System;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain
{
	public class Service
	{
		public StreamingServiceType Type { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }
	}
}

