using System;
using MShare.Framework.Api;

namespace MShare.Framework.Infrastructure
{
    public class ExecutionContext : IExecutionContext
    {
        public string OsVersion { get; set; }
        public string Os { get; set; }
        public string DeviceId { get; set; }
    }
}

