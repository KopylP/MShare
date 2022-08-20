using System;
using MediatR;

namespace MShare.Framework.Api
{
	public interface IQuery<T> : IRequest<T>
    {
	}
}

