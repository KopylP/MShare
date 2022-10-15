using Microsoft.EntityFrameworkCore;
using MShare.Framework.Domain;

namespace MShare.Framework.Infrastructure.Persistance.EntityFramework
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context) => _context = context;

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification)
        {
            return await ListQuery(specification).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(ISpecification<TEntity>? specification = default)
        {
            specification ??= SpecificationBase<TEntity>.All;
            return await ListQuery(specification).ToArrayAsync();
        }

        public async Task SaveAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public IQueryable<TEntity> ListQuery(ISpecification<TEntity> specification)
        {
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(_context.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = specification.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return secondaryResult
                .Where(specification.Criteria)
                .AsQueryable();
        }
    }
}

