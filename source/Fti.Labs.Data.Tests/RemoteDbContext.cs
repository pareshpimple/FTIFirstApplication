namespace Fti.Labs.Data.Tests
{
	using Dapper;
	using Relativity.API;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Common;
	using System.Data.SqlClient;
	using System.Linq;

	public class RemoteDbContext : IDBContext
	{
		private SqlConnection connection;
		private SqlTransaction transaction;

		public RemoteDbContext(SqlConnection connection)
		{
			this.connection = connection;
			Database = connection.Database;
			ServerName = connection.DataSource;
			IsMasterDatabase = connection.DataSource.ToLower() == "edds";
		}

		public string Database { get; private set; }
		public string ServerName { get; private set; }
		public bool IsMasterDatabase { get; private set; }

		public SqlConnection GetConnection()
		{
			return connection;
		}

		public DbParameter CreateDbParameter()
		{
			throw new NotImplementedException();
		}

		public SqlConnection GetConnection(bool openConnectionIfClosed)
		{
			return connection;
		}

		public SqlTransaction GetTransaction()
		{
			return transaction;
		}

		public void BeginTransaction()
		{
			connection.Open();
			transaction = connection.BeginTransaction();
		}

		public void CommitTransaction()
		{
			transaction.Commit();
			transaction = null;
			connection.Close();
		}

		public void RollbackTransaction()
		{
			try
			{
				transaction.Rollback();
				transaction = null;
				connection.Close();
			}
			catch
			{
				try
				{
					// try to close the connection in case there is an error during rollback
					connection.Close();
				}
				catch { }

				throw;
			}
		}

		public void RollbackTransaction(Exception originatingException) => RollbackTransaction();

		public void ReleaseConnection() => throw new NotImplementedException();

		public void Cancel() => throw new NotImplementedException();

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement) => throw new NotImplementedException();

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, int timeoutValue) => throw new NotImplementedException();

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, IEnumerable<SqlParameter> parameters) => throw new NotImplementedException();

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, int timeoutValue, IEnumerable<SqlParameter> parameters) => throw new NotImplementedException();

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement) => connection.QueryFirstOrDefault<T>(sqlStatement, null, this.transaction);

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, IEnumerable<SqlParameter> parameters)
		{
			var dynParams = new DynamicParameters(new { });
			parameters.ToList().ForEach(p => dynParams.Add(p.ParameterName, p.Value));
			return connection.QueryFirstOrDefault<T>(sqlStatement, dynParams, this.transaction);
		}


		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, int timeoutValue) => connection.QueryFirstOrDefault<T>(sqlStatement, null, this.transaction);

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, IEnumerable<SqlParameter> parameters, int timeoutValue) => throw new NotImplementedException();

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, params SqlParameter[] parameters) =>
			ExecuteSqlStatementAsScalar<T>(sqlStatement, parameters.AsEnumerable());

		public object ExecuteSqlStatementAsScalar(string sqlStatement, params SqlParameter[] parameters) => throw new NotImplementedException();

		public object ExecuteSqlStatementAsScalar(string sqlStatement, IEnumerable<SqlParameter> parameters, int timeoutValue) => throw new NotImplementedException();

		public object ExecuteSqlStatementAsScalarWithInnerTransaction(string sqlStatement, IEnumerable<SqlParameter> parameters, int timeoutValue) => throw new NotImplementedException();

		public int ExecuteNonQuerySQLStatement(string sqlStatement) => connection.Execute(sqlStatement, null, this.transaction);

		public int ExecuteNonQuerySQLStatement(string sqlStatement, int timeoutValue) => connection.Execute(sqlStatement, null, this.transaction);

		public int ExecuteNonQuerySQLStatement(string sqlStatement, IEnumerable<SqlParameter> parameters)
		{
			var dynParams = new DynamicParameters(new { });
			parameters.ToList().ForEach(p => dynParams.Add(p.ParameterName, p.Value));
			return connection.Execute(sqlStatement, dynParams, transaction);
		}

		public int ExecuteNonQuerySQLStatement(string sqlStatement, IEnumerable<SqlParameter> parameters, int timeoutValue) => throw new NotImplementedException();

		public DbDataReader ExecuteSqlStatementAsDbDataReader(string sqlStatement) => throw new NotImplementedException();

		public DbDataReader ExecuteSqlStatementAsDbDataReader(string sqlStatement, int timeoutValue) => throw new NotImplementedException();

		public DbDataReader ExecuteSqlStatementAsDbDataReader(string sqlStatement, IEnumerable<DbParameter> parameters) => throw new NotImplementedException();

		public DbDataReader ExecuteSqlStatementAsDbDataReader(string sqlStatement, IEnumerable<DbParameter> parameters, int timeoutValue) => throw new NotImplementedException();

		public DataTable ExecuteSQLStatementGetSecondDataTable(string sqlStatement, int timeout = -1) => throw new NotImplementedException();

		public SqlDataReader ExecuteSQLStatementAsReader(string sqlStatement, int timeout = -1) => throw new NotImplementedException();

		public IEnumerable<T> ExecuteSQLStatementAsEnumerable<T>(string sqlStatement, Func<SqlDataReader, T> converter, int timeout = -1) => throw new NotImplementedException();

		public DbDataReader ExecuteProcedureAsReader(string procedureName, IEnumerable<SqlParameter> parameters) => throw new NotImplementedException();

		public int ExecuteProcedureNonQuery(string procedureName, IEnumerable<SqlParameter> parameters) => throw new NotImplementedException();

		public SqlDataReader ExecuteParameterizedSQLStatementAsReader(string sqlStatement, IEnumerable<SqlParameter> parameters,
			int timeoutValue = -1, bool sequentialAccess = false) => throw new NotImplementedException();

		public DataSet ExecuteSqlStatementAsDataSet(string sqlStatement) => throw new NotImplementedException();

		public DataSet ExecuteSqlStatementAsDataSet(string sqlStatement, IEnumerable<SqlParameter> parameters) => throw new NotImplementedException();

		public DataSet ExecuteSqlStatementAsDataSet(string sqlStatement, int timeoutValue) => throw new NotImplementedException();

		public DataSet ExecuteSqlStatementAsDataSet(string sqlStatement, IEnumerable<SqlParameter> parameters, int timeoutValue) => throw new NotImplementedException();
	}
}
