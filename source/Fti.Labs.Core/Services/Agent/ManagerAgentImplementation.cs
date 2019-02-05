namespace Fti.Labs.Core.Services.Agent
{

	using kCura.Relativity.Client.DTOs;
	using Fti.Labs.Core.Extensions;
	using Fti.Labs.Core.Interfaces;
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Core.Models;
	using Fti.Labs.Core.Constants;
	using Serilog;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class ManagerAgentImplementation : AgentBase
	{
		private readonly ILogger logger;
		private readonly IDocumentRepository documentRepository;
		private readonly IDocumentQueueRepository documentQueueRepository;
		private readonly IWorkspaceRepository workspaceRepository;
		private readonly IDocProcessingJobRepository docProcessingJobRepository;

		public ManagerAgentImplementation(
			IAgent agent,
			ILogger logger,
			IDocumentRepository documentRepository,
			IDocumentQueueRepository documentQueueRepository,
			IWorkspaceRepository workspaceRepository,
			IDocProcessingJobRepository docProcessingJobRepository)
			: base(agent, logger)
		{
			this.logger = logger;
			this.documentRepository = documentRepository;
			this.documentQueueRepository = documentQueueRepository;
			this.workspaceRepository = workspaceRepository;
			this.docProcessingJobRepository = docProcessingJobRepository;
		}

		internal override Task ExecuteFtiAgent(CancellationToken token)
		{
			logger.Information($"Starting manager.");

			// Read workspaces with application
			var workspaces = workspaceRepository.ReadAllWithApplication(Guids.Application.ApplicationGuid);

			if (!workspaces.Any())
			{
				logger.Warning($"No workspaces with application.");
				return Task.CompletedTask;
			}

			workspaces.ForEach(ProcessJobsInWorkspace);

			logger.Information("Completed manager.");

			return Task.CompletedTask;
		}

		internal void ProcessJobsInWorkspace(Workspace workspace)
		{
			// Look for pending jobs
			var job = docProcessingJobRepository.ReadNextPendingJob();

			if (job != null)
			{
				// enqueue work for worker agent
				var documents = documentRepository.Search(job.WorkspaceId, job.SearchId);
				documents
					.Select(doc => new DocumentProcessingQueue { DocumentId = doc.ArtifactID, WorkspaceId = job.WorkspaceId, Status = 0 })
					.ForEach(d => documentQueueRepository.Create(d));
			}
		}
	}
}
