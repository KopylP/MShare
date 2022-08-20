using System;
using MShare.Framework.Api;

namespace MShare.Framework.Infrastructure
{
	public interface IContextExecutor
	{
		Task<T> ExecuteAsync<T>(IQuery<T> query);
		Task<T> ExecuteAsync<T>(ICommand<T> command);
    }
}

