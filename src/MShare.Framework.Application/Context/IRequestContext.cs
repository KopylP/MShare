using System;
using MediatR;

namespace MShare.Framework.Application.Context
{
	public interface IRequestContext<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
	}
}

