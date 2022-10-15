using System;
using MassTransit;
using MediatR;

namespace MShare.Framework.Api
{
	public interface IConsumerContextExecutor
	{
        Task<T> ExecuteAsync<T>(IQuery<T> query, ConsumeContext? context = default);
        Task<Unit> ExecuteAsync(ICommand command, ConsumeContext? context = default);
    }
}

