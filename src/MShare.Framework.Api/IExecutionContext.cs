using System;
using MShare.Framework.Types.Addresses;

namespace MShare.Framework.Api
{
	public interface IExecutionContext
	{
		public string OsVersion { get; }
		public string Os { get; }
		public string DeviceId { get; }
        public string StoreRegion { get; }
		public string UserLocate { get; }
    }
}

