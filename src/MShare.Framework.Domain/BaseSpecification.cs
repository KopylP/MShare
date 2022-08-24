using System;
using System.Linq.Expressions;
using MShare.Framework.Expressions;

namespace MShare.Framework.Domain
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public static ISpecification<T> All => new AllSpecification<T>();

        public static ISpecification<T> Nothing => new NothingSpecification<T>();

        public abstract Expression<Func<T, bool>> Criteria { get; }

        public virtual List<Expression<Func<T, object>>> Includes { get; } =
                                               new List<Expression<Func<T, object>>>();
        public virtual List<string> IncludeStrings { get; } = new List<string>();

        public ISpecification<T> And(ISpecification<T> another)
            => new AndSpecification<T>(this, another);

        public ISpecification<T> Or(ISpecification<T> another)
            => new OrSpecification<T>(this, another);

        public bool IsSatisfiedBy(T obj) => Criteria.Compile()(obj);  

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }

    class AllSpecification<T> : BaseSpecification<T>
    {
        public override Expression<Func<T, bool>> Criteria => p => true;
    }

    class NothingSpecification<T> : BaseSpecification<T>
    {
        public override Expression<Func<T, bool>> Criteria => p => false;
    }

    public abstract class BaseCompositeSpecification<T> : BaseSpecification<T>
    {
        protected readonly ISpecification<T> _firstSpecification;
        protected readonly ISpecification<T> _secondSpecification;

        private readonly List<Expression<Func<T, object>>> _includes;
        private readonly List<string> _includeStrings;

        public BaseCompositeSpecification(ISpecification<T> firstSpec, ISpecification<T> secondSpec)
        {
            _firstSpecification = firstSpec;
            _secondSpecification = secondSpec;

            _includes = _firstSpecification.Includes;
            _includes.AddRange(_secondSpecification.Includes);

            _includeStrings = firstSpec.IncludeStrings;
            _includeStrings.AddRange(secondSpec.IncludeStrings);
        }

        public override List<Expression<Func<T, object>>> Includes => _includes;
        public override List<string> IncludeStrings => _includeStrings;
    }

    public class AndSpecification<T> : BaseCompositeSpecification<T>
    {
        public AndSpecification(ISpecification<T> firstSpec, ISpecification<T> secondSpec)
            : base(firstSpec, secondSpec)
        {
        }

        public override Expression<Func<T, bool>> Criteria => _firstSpecification.Criteria
            .And(_secondSpecification.Criteria);
    }

    public class OrSpecification<T> : BaseCompositeSpecification<T>
    {
        public OrSpecification(ISpecification<T> firstSpec, ISpecification<T> secondSpec)
            : base(firstSpec, secondSpec)
        {
        }

        public override Expression<Func<T, bool>> Criteria => _firstSpecification.Criteria
            .Or(_secondSpecification.Criteria);
    }
}

