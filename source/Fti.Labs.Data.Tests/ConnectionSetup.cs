using System.Collections;
using System.Collections.Generic;
using Fti.Labs.Data.Services;
using Relativity.API;

namespace Fti.Labs.Data.Tests
{
	using Dapper;
	using System;
	using System.Data.SqlClient;
	using System.IO;

	public static class ConnectionSetup
	{
		public static SqlConnection GetConnection()
		{
			var dbName = Guid.NewGuid().ToString("N");
			using (var masterConnection =
				new SqlConnection(BaseLocalDbConnection))
			{
				masterConnection.Execute($@"
					CREATE DATABASE
						[{dbName}]
					ON PRIMARY (
						NAME=Test_data,
						FILENAME = '{Path.GetTempFileName()}.{dbName}.mdf'
					)
					LOG ON (
						NAME=Test_log,
						FILENAME = '{Path.GetTempFileName()}.{dbName}_log.ldf'
					)");
			}

			// Track the db name so we can attempt to delete it on test teardown
			DatabaseNames.Add(dbName);

			var builder = new SqlConnectionStringBuilder(BaseLocalDbConnection);
			builder.InitialCatalog = dbName;
			return new SqlConnection(builder.ToString());
		}

		internal static void DeployDatabase(IHelper helper)
		{
			var service = new DatabaseDeploymentService(helper);
			service.DeployDatabase();
		}

		public static void DeleteDatabases()
		{
			foreach (var databaseName in DatabaseNames)
			{
				try
				{
					using (var connection = new SqlConnection(BaseLocalDbConnection))
					{
						connection.Open();
						var exists =
							connection.QueryFirstOrDefault<int?>($"select 1 from sys.databases where name = 'databaseName'");
						if (exists.HasValue && exists >= 1)
						{
							connection.Execute($"alter database [{databaseName}] set single_user with rollback immediate");
							connection.Execute($"ALTER DATABASE [{databaseName}] SET OFFLINE");
							connection.Execute($"drop database [{databaseName}]");
						}

					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Couldn't delete database {databaseName}: {ex}");
				}
			}
		}

		private static readonly IList<string> DatabaseNames = new List<string>();

		private static string BaseLocalDbConnection =>
			@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Connect Timeout = 3;";
	}
}
