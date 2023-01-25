using AutoMapper;
using MShare.Framework.Infrastructure.AccessToken;
using MShare.Framework.Types;
using MShare.Framework.Types.Addresses;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
    public partial class AppleClient : IStreamingServiceClient
    {
        private readonly string _publicApiUrl;
        private readonly IMapper _mapper;
        private readonly IAccessTokenProvider _accessTokenProvider;


        public AppleClient(IConfiguration configuration, IMapper mapper, IAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
            _publicApiUrl = configuration.GetValue<string>("ApplePublicApiUrl");
            _mapper = mapper;
        }

        public Task<AlbumResponseDto> GetAlbumByUpc(GetByUpcRequestDto request)
        {
            throw new NotImplementedException();
        }

        private string GetRegion(string region) => region != CountryCode2.Invariant.ToString() ? region : ApiConstants.DefaultRegion;
    }
}