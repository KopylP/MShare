using System;
using System.Linq.Expressions;
using MShare.Framework.Domain.Exceptions;
using MShare.Framework.Exceptions;

namespace MShare.Framework.Domain
{
	public abstract class EntityBase : IEntity
	{
		protected virtual void OnSaving()
		{
		}

		protected virtual void Check(bool isCorrect, string message)
        {
			Thrower.ThrowIf<DomainException>(!isCorrect, message);
		}
	}
}

