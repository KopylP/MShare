using System;
using MassTransit;
using MediatR;
using MShare.Framework.Api;
using MShare.Framework.Infrastructure.Execution;

namespace MShare.Framework.Infrastructure.Messaging
{
    public class ConsumerContextExecutor : IConsumerContextExecutor
    {
        private readonly IExecutionContextUpdater _updater;
        private readonly IMediator _mediator;

        public ConsumerContextExecutor(IExecutionContextUpdater updater, IMediator mediator)
        {
            _updater = updater;
            _mediator = mediator;
        }

        public async Task<T> ExecuteAsync<T>(IQuery<T> query, ConsumeContext? context = null)
        {
            return await Execute(query, context);
        }

        public async Task<T> ExecuteAsync<T>(ICommand<T> command, ConsumeContext? context = null)
        {
            return await Execute(command, context);
        }

        private async Task<T> Execute<T>(IRequest<T> request, ConsumeContext? context = null)
        {
            if (context is not null)
            {
                _updater.Update(context);
            }

            return await _mediator.Send(request);
        }
    }
}

