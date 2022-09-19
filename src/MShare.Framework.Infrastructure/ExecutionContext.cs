using System;
using MShare.Framework.Api;
using MShare.Framework.Types.Addresses;

namespace MShare.Framework.Infrastructure
{
    internal class ExecutionContext : IExecutionContext
    {
        public string OsVersion { get; set; }
        public string Os { get; set; }
        public string DeviceId { get; set; }
        public CountryCode2 StoreRegion { get; set; }
    }
}

