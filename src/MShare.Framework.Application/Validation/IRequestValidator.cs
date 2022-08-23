using System;
using MediatR;

namespace MShare.Framework.Application.Validation
{
	public interface IRequestValidator<TRequest, TResonse> where TRequest : IRequest<TResonse>
	{
        void Validate(TRequest request);
    }
}

