using System;
using MediatR;
using MShare.Framework.Api;

namespace MShare.Framework.Application
{
	public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
	{
	}
}

