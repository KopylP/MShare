using System;
using Dapper;

namespace MShare.Framework.Application.SqlClient
{
	public abstract class SqlBuilderBase 
	{
		private readonly SqlBuilder _sqlBuilder = new SqlBuilder();

		public SqlBuilderBase()
		{
		}

        protected virtual void ApplyFilters(FilterBuilder filterBuilder)
        {
        }

		protected virtual void ApplySelect(SelectBuilder selectBuilder)
		{
		}

		protected abstract string GetTamplate();

		public (string sql, object parameters) Build()
		{
			ApplySelect(new SelectBuilder(_sqlBuilder));
			ApplyFilters(new FilterBuilder(_sqlBuilder));
			var template = _sqlBuilder.AddTemplate(GetTamplate());

			return (sql: template.RawSql, parameters: template.Parameters);
		}

		public class FilterBuilder
		{
			private readonly SqlBuilder _sqlBuilder;

			internal FilterBuilder(SqlBuilder sqlBuilder)
			{
				_sqlBuilder = sqlBuilder;
			}

			public void Where(string clause, dynamic? parameters = default)
			{
				_sqlBuilder.Where(clause, parameters);
			}

            public void WhereIf(bool condition, string clause, dynamic? parameters = default)
            {
				if (condition)
					_sqlBuilder.Where(clause, parameters);
            }
        }

        public class SelectBuilder
        {
            private readonly SqlBuilder _sqlBuilder;

            internal SelectBuilder(SqlBuilder sqlBuilder)
            {
                _sqlBuilder = sqlBuilder;
            }

            public void Select(string sql)
            {
                _sqlBuilder.Select(sql);
            }
        }
    }
}

