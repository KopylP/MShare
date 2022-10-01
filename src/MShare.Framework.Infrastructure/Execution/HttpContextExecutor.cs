using System;
using MShare.Framework.Api;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using MediatR;
using MShare.Framework.Types.Addresses;

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

        public Task<T> ExecuteAsync<T>(ICommand<T> command) => ExecuteAsync((IRequest<T>)command);

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

            if (_executionContext is ExecutionContext executionContext)
            {
                executionContext.Os = os;
                executionContext.OsVersion = osVersion;
                executionContext.DeviceId = deviceId;
                executionContext.StoreRegion = CountryCode2.Of(string.IsNullOrEmpty(region.ToString())? "US": region);
            }
        }
    }
}
