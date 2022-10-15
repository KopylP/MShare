using System;
using MediatR;
using MShare.Framework.Api;

namespace MShare.Framework.Api
{
	public interface IHttpContextExecutor
	{
		Task<T> ExecuteAsync<T>(IQuery<T> query);
		Task<Unit> ExecuteAsync(ICommand command);
    }
}

