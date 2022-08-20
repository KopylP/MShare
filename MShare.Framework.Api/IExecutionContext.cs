using System;
namespace MShare.Framework.Api
{
	public interface IExecutionContext
	{
		public string OsVersion { get; }
		public string Os { get; }
		public string DeviceId { get; }
	}
}

