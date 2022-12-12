using System;
using MShare.Framework.Api;
using MShare.Framework.Types.Addresses;

namespace MShare.Framework.Infrastructure.Execution
{
    internal record ExecutionContext : IExecutionContext
    {
        public string OsVersion { get; set; }
        public string Os { get; set; }
        public string DeviceId { get; set; }
        public string StoreRegion { get; set; }
        public string UserLocate { get; set; }

        public void UpdateFrom(IExecutionContext context)
        {
            OsVersion = context.OsVersion;
            Os = context.Os;
            DeviceId = context.DeviceId;
            StoreRegion = context.StoreRegion;
            UserLocate = context.UserLocate;
        }
    }
}

