using System;
using MediatR;

namespace MShare.Framework.Api
{
	public interface ICommand : IRequest<Unit>
	{
	}
}

