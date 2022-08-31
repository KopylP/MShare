using AutoMapper;
using MShare.Framework.Types;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
    public partial class AppleClient : IStreamingServiceClient
    {
        private readonly string _publicApiUrl;
        private readonly IMapper _mapper;

        private (int RetryCount, IntRange waitRangeInMilliseconds) _retryPolicy => (2, IntRange.Of(100, 300));

        public AppleClient(IConfiguration configuration, IMapper mapper)
        {
            _publicApiUrl = configuration.GetValue<string>("ApplePublicApiUrl");
            _mapper = mapper;
        }
    }
}

