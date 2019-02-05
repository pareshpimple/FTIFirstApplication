namespace Fti.Labs.Data.Services
{
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Data.Properties;
	using Relativity.API;
	using System.Collections.Generic;

	public class DatabaseDeploymentService : IDatabaseDeploymentService
	{
		private readonly IHelper helper;

		public DatabaseDeploymentService(IHelper helper)
		{
			this.helper = helper;
		}

		public void DeployDatabase()
		{
			var context = helper.GetDBContext(-1);
			try
			{
				foreach (var migrationScript in MigrationScripts)
				{
					context.BeginTransaction();
					context.ExecuteNonQuerySQLStatement(migrationScript);
					context.CommitTransaction();
				}
			}
			catch
			{
				context.RollbackTransaction();
				throw;
			}
		}

		private static readonly IList<string> MigrationScripts = new[]
		{
			Resources._0001_Create_Application_Schema,
			Resources._0002_Create_QueueTable
		};
	}
}
