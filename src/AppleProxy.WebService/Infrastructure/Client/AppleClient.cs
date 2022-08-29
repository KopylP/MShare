using AutoMapper;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
    public partial class AppleClient : IStreamingServiceClient
    {
        private readonly string _publicApiUrl;
        private readonly IMapper _mapper;

        private (int RetryCount, int waitInMilliseconds) _retryPolicy => (2, 200);

        public AppleClient(IConfiguration configuration, IMapper mapper)
        {
            _publicApiUrl = configuration.GetValue<string>("ApplePublicApiUrl");
            _mapper = mapper;
        }
    }
}

