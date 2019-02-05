namespace Fti.Labs.Data.Repositories
{
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Core.Models;
	using Relativity.API;
	using System.Data.SqlClient;

	public class DocumentQueueRepository : IDocumentQueueRepository
	{
		private readonly IHelper helper;

		public DocumentQueueRepository(IHelper helper)
		{
			this.helper = helper;
		}

		public void Create(DocumentProcessingQueue documentProcessingQueue)
		{
			var context = helper.GetDBContext(-1);
			try
			{
				context.BeginTransaction();
				context.ExecuteNonQuerySQLStatement(
					@"	insert into labs.DocumentProcessingQueue
					(WorkspaceID, DocumentID, Status)
					values (@WorkspaceId, @DocumentId, 0)",
					new[]
					{
						new SqlParameter("WorkspaceId", documentProcessingQueue.WorkspaceId),
						new SqlParameter("DocumentId", documentProcessingQueue.DocumentId)
					});
				context.CommitTransaction();
			}
			catch 
			{
				context.RollbackTransaction();
				throw;
			}
		}

		public DocumentProcessingQueue ReadNextFromQueue()
		{
			var context = helper.GetDBContext(-1);

			try
			{
				context.BeginTransaction();
				// Read the Id of th next item in te queue
				var nextId = context.ExecuteSqlStatementAsScalar<int?>(
					@"select top(1) ID from labs.DocumentProcessingQueue
					where Status = 0
					order by Id asc");

				if (!nextId.HasValue)
					return null;

				// Update the status to in progress
				context.ExecuteNonQuerySQLStatement(
					@"	update labs.DocumentProcessingQueue
						set Status = 1
						where ID = @Id",
					new[] { new SqlParameter("Id", nextId.Value) });

				// Read the full details
				var next = context.ExecuteSqlStatementAsScalar<DocumentProcessingQueue>(
					@"	select top(1) * from labs.DocumentProcessingQueue
						where ID = @Id",
					new SqlParameter("Id", nextId));

				context.CommitTransaction();

				return next;
			}
			catch
			{
				context.RollbackTransaction();
				throw;
			}
		}
	}
}
