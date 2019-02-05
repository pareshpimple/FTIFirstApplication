namespace Fti.Labs.Core.Tests.Services.Agent
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fti.Labs.Core.Interfaces;
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Core.Models;
	using Fti.Labs.Core.Services.Agent;
	using Fti.Labs.Core.Services;
	using kCura.Relativity.Client.DTOs;
	using Moq;
	using NUnit.Framework;
	using Serilog;


	[TestFixture]
	public class WorkerAgentImplementationTests
	{
		[SetUp]
		public void Setup()
		{
			this.agent = new Mock<IAgent>();
			logger = LoggerFactory.CreateDefaultLogger();
			this.documentRepository = new Mock<IDocumentRepository>();
			this.documentQueueRepository = new Mock<IDocumentQueueRepository>();
			this.documentProcessingService = new Mock<IDocumentProcessingService>();
			workerAgentImplementation = new WorkerAgentImplementation(
				agent.Object,
				logger,
				documentRepository.Object,
				documentQueueRepository.Object,
				documentProcessingService.Object);
		}

		private Mock<IAgent> agent;
		private ILogger logger;
		private Mock<IDocumentRepository> documentRepository;
		private Mock<IDocumentQueueRepository> documentQueueRepository;
		private Mock<IDocumentProcessingService> documentProcessingService;
		private WorkerAgentImplementation workerAgentImplementation;

		[Test]
		public async Task WorkerAgent_Execute()
		{
			// Arrange
			var workspaceId = 1234;
			var documentId = 444;

			documentQueueRepository.Setup(r => r.ReadNextFromQueue())
				.Returns(new DocumentProcessingQueue{ WorkspaceId = workspaceId, DocumentId = documentId });

			documentProcessingService.Setup(r => r.ProcessDocument(It.IsAny<Document>()))
				.Returns(Task.CompletedTask);

			documentRepository.Setup(r => r.Read(workspaceId, documentId))
				.Returns(new Document(documentId));

			// Act
			await workerAgentImplementation.ExecuteFtiAgent(new CancellationToken());

			// Assert
			documentProcessingService.Verify(r => r.ProcessDocument(It.IsAny<Document>()));
		}
	}
}
