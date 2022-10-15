using System;
using MediatR;
using MShare.Framework.Api;

namespace MShare.Framework.Application
{
	public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Unit>
		where TCommand : ICommand
	{
	}
}

