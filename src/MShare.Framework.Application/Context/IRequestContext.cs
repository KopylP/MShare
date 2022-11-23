using System;
using MediatR;

namespace MShare.Framework.Application.Context
{
	public interface IQueryContext<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
	}
}

