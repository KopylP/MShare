using System;
using MediatR;

namespace MShare.Framework.Application.Actions
{
    public interface IAfterActionHandler<TRequest, TResonse> where TRequest : IRequest<TResonse>
    {
        Task Handle(TRequest request, TResonse resonse);
    }
}

