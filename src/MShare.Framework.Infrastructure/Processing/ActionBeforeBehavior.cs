using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Application.Actions;

namespace MShare.Framework.Infrastructure.Processing
{
    public class BeforeActionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public BeforeActionBehavior(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var beforeActions = _serviceProvider.GetService<IEnumerable<IBeforeActionHandler<TRequest, TResponse>>>();

            if (beforeActions?.Any() ?? false)
                foreach (var beforeAction in beforeActions)
                    await beforeAction.Handle(request);

            return await next();
        }
    }
}
