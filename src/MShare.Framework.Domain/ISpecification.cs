using System;
using System.Linq.Expressions;

namespace MShare.Framework.Domain
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        ISpecification<T> And(ISpecification<T> another);
        ISpecification<T> Or(ISpecification<T> another);
        public bool IsSatisfiedBy(T obj);
    }
}