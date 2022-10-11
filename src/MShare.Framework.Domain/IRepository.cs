using System;
namespace MShare.Framework.Domain
{
	public interface IRepository<T> where T: IEntity
	{
		Task<IEnumerable<T>> GetListAsync(ISpecification<T>? specification = default);
		Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);
		Task SaveAsync(T entity);
		Task DeleteAsync(T entity);
    }
}