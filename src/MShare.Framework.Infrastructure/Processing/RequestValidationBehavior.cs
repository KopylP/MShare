using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Application.Validation;

namespace MShare.Framework.Infrastructure.Processing
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestValidationBehavior(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validators = _serviceProvider.GetService<IEnumerable<IRequestValidator<TRequest, TResponse>>>();

            if (validators?.Any() ?? false)
                foreach (var validator in validators)
                    validator.Validate(request);

            return await next();
        }
    }
}

