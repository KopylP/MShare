using System;
using MediatR;

namespace MShare.Framework.Api
{
	public interface ICommand<T> : IRequest<T>
	{
	}
}

