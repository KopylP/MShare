using System;
using MShare.Framework.Api;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using MediatR;
using MShare.Framework.Types.Addresses;
using MShare.Framework.Api.Exceptions;

namespace MShare.Framework.Infrastructure.Execution
{
	public class HttpContextExecutor : IHttpContextExecutor
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExecutionContext _executionContext;
        private readonly IMediator _mediator;

        public HttpContextExecutor(IHttpContextAccessor accessor, IExecutionContext executionContext, IMediator mediator)
            => (_httpContextAccessor, _executionContext, _mediator) = (accessor, executionContext, mediator);

        public Task<T> ExecuteAsync<T>(IQuery<T> query) => ExecuteAsync((IRequest<T>)query);

        public Task<Unit> ExecuteAsync(ICommand command) => ExecuteAsync((IRequest<Unit>)command);

        private async Task<T> ExecuteAsync<T>(IRequest<T> request)
        {
            SetUpContext();
            return await _mediator.Send(request);
        }

        private void SetUpContext()
        {
            var os = _httpContextAccessor.HttpContext.Request.Headers["mshare-os-name"];
            var osVersion = _httpContextAccessor.HttpContext.Request.Headers["mshare-os-version"];
            var deviceId = _httpContextAccessor.HttpContext.Request.Headers["mshare-device-id"];
            var region = _httpContextAccessor.HttpContext.Request.Headers["mshare-store-region"];
            var locate = _httpContextAccessor.HttpContext.Request.Headers["mshare-user-locate"];

            if (_executionContext is ExecutionContext executionContext)
            {
                executionContext.Os = os;
                executionContext.OsVersion = osVersion;
                executionContext.DeviceId = deviceId;
                executionContext.StoreRegion = GetStoreRegion(region, locate);
                executionContext.UserLocate = GetLocate(locate);
            }
        }

        private string GetStoreRegion(string region, string locate)
        {
            try
            {
                region = !string.IsNullOrEmpty(region) ? region : locate;

                if (string.IsNullOrEmpty(region))
                    return CountryCode2.Invariant;

                if (region.Length == 3)
                    return CountryCodeConverter.To2(CountryCode3.Of(region));

                return CountryCode2.Of(region);
            }
            catch (ArgumentException)
            {
                throw new BadRequestException("Store region is invalid");
            }
        }

        private string GetLocate(string? locate)
        {
            try
            {
                if (string.IsNullOrEmpty(locate))
                    return CountryCode2.Invariant.Code;

                return CountryCode2.Of(locate).Code;
            }
            catch (ArgumentException)
            {
                throw new BadRequestException("Locate is invalid");
            }
        }
    }
}
