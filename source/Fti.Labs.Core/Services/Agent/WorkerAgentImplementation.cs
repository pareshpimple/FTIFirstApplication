namespace Fti.Labs.Core.Services.Agent
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Fti.Labs.Core.Interfaces;
	using Fti.Labs.Core.Interfaces.Data;
	using Serilog;

	public class WorkerAgentImplementation : AgentBase
	{
		private readonly ILogger logger;
		private readonly IDocumentRepository documentRepository;
		private readonly IDocumentQueueRepository documentQueueRepository;
		private readonly IDocumentProcessingService documentProcessingService;

		public WorkerAgentImplementation(
			IAgent agent,
			ILogger logger,
			IDocumentRepository documentRepository,
			IDocumentQueueRepository documentQueueRepository,
			IDocumentProcessingService documentProcessingService)
			: base(agent, logger)
		{
			this.logger = logger;
			this.documentRepository = documentRepository;
			this.documentQueueRepository = documentQueueRepository;
			this.documentProcessingService = documentProcessingService;
		}

		internal override async Task ExecuteFtiAgent(CancellationToken token)
		{
			this.logger.Information("Starting worker.");

			// Get next document to process
			var nextItem = this.documentQueueRepository.ReadNextFromQueue();

			if (nextItem != null)
			{
				// Read the document
				var document = this.documentRepository.Read(nextItem.WorkspaceId, nextItem.DocumentId);

				// Do stuff with the document
				await this.documentProcessingService.ProcessDocument(document);
			}

			this.logger.Information("Completed worker.");
		}
	}
}
