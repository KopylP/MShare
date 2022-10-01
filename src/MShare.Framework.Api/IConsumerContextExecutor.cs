using System;
using MassTransit;

namespace MShare.Framework.Api
{
	public interface IConsumerContextExecutor
	{
        Task<T> ExecuteAsync<T>(IQuery<T> query, ConsumeContext? context = default);
        Task<T> ExecuteAsync<T>(ICommand<T> command, ConsumeContext? context = default);
    }
}

