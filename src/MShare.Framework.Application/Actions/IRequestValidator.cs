using System;
using MediatR;

namespace MShare.Framework.Application.Actions
{
	public interface IRequestValidator<TRequest, TResonse> where TRequest : IRequest<TResonse>
	{
        Task Validate(TRequest request);
    }
}

