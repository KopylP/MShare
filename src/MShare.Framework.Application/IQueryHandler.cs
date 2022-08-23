using System;
using MediatR;

namespace MShare.Framework.Application
{
	public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
		where TQuery: IRequest<TResponse>
	{
	}
}

