using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Application.Actions;

namespace MShare.Framework.Infrastructure.Processing
{
	public class AfterActionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public AfterActionBehavior(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            var afterActions = _serviceProvider.GetService<IEnumerable<IAfterActionHandler<TRequest, TResponse>>>();

            if (afterActions?.Any() ?? false)
                foreach (var afterAction in afterActions)
                    await afterAction.Handle(request, response);

            return response;
        }
    }
}

