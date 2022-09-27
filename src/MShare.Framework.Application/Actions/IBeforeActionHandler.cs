using System;
using MediatR;

namespace MShare.Framework.Application.Actions
{
	public interface IBeforeActionHandler<TRequest, TResonse> where TRequest : IRequest<TResonse>
    {
        void Handle(TRequest request);
    }
}

