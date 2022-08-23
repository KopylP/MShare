using System;
using System.Linq.Expressions;

namespace MShare.Framework.Types
{
	public abstract class SimpleSpecification<TType>
	{
        public abstract Func<TType, bool> Clause { get; }

        public bool IsSatisfiedBy(TType obj) => Clause(obj);
    }
}

