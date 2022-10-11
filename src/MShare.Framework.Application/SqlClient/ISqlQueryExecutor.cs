using System;
using System.Data;

namespace MShare.Framework.Application.SqlClient
{
	public interface ISqlQueryExecutor : IDisposable
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default);
        Task<T> QuerySingleAsync<T>(string sql, object? param = default, CancellationToken cancellationToken = default);
    }
}