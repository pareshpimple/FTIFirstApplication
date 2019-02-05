using Fti.Labs.Core.Constants;
using Fti.Labs.Core.Interfaces;
using Fti.Labs.Core.Interfaces.Data;
using Fti.Labs.Core.Models;
using Fti.Labs.Core.Services.Agent;
using kCura.Relativity.Client.DTOs;
using Moq;
using NUnit.Framework;
using Serilog;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fti.Labs.Core.Services;

namespace Fti.Labs.Core.Tests.Services.Agent
{
	[TestFixture]
	public class ManagerAgentImplementationTests
	{
		[SetUp]
		public void Setup()
		{
			logger = LoggerFactory.CreateDefaultLogger();
			agent = new Mock<IAgent>();
			documentRepository = new Mock<IDocumentRepository>();
			documentQueueRepository = new Mock<IDocumentQueueRepository>();
			workspaceRepository = new Mock<IWorkspaceRepository>();
			docProcessingJobRepository = new Mock<IDocProcessingJobRepository>();
			managerAgentImplementation = new ManagerAgentImplementation(
				agent.Object,
				logger,
				documentRepository.Object,
				documentQueueRepository.Object,
				workspaceRepository.Object,
				docProcessingJobRepository.Object);
		}

		private ILogger logger;
		private Mock<IAgent> agent;
		private Mock<IDocumentRepository> documentRepository;
		private Mock<IDocumentQueueRepository> documentQueueRepository;
		private Mock<IWorkspaceRepository> workspaceRepository;
		private Mock<IDocProcessingJobRepository> docProcessingJobRepository;
		private ManagerAgentImplementation managerAgentImplementation;

		[Test]
		public async Task ManagerAgent_Execute()
		{
			// Arrange
			var workspaceId = 1234;
			var searchId = 444;
			var documents = Enumerable.Range(1, 10).Select(i => new Document(100 + i)).ToList();

			workspaceRepository.Setup(r => r.ReadAllWithApplication(Guids.Application.ApplicationGuid))
				.Returns(new[] { new Workspace(workspaceId) });

			docProcessingJobRepository.Setup(r => r.ReadNextPendingJob())
				.Returns(new DocumentProcessingJob { SearchId = searchId, WorkspaceId = workspaceId });

			documentRepository.Setup(r => r.Search(workspaceId, searchId))
				.Returns(documents);

			// Act
			await managerAgentImplementation.ExecuteFtiAgent(new CancellationToken());

			// Assert
			documentQueueRepository.Verify(repo => repo.Create(It.IsAny<DocumentProcessingQueue>()), Times.Exactly(10));
		}

		[Test]
		public void ManagerAgent_ProcessJobsInWorkspace()
		{
			// Arrange
			var workspaceId = 1234;
			var searchId = 444;
			var documents = Enumerable.Range(1, 10).Select(i => new Document(100 + i)).ToList();
			docProcessingJobRepository.Setup(r => r.ReadNextPendingJob())
				.Returns(new DocumentProcessingJob { SearchId = searchId, WorkspaceId = workspaceId });

			documentRepository.Setup(r => r.Search(workspaceId, searchId))
				.Returns(documents);

			// Act
			managerAgentImplementation.ProcessJobsInWorkspace(new Workspace(workspaceId));

			// Assert
			documentQueueRepository.Verify(repo => repo.Create(It.IsAny<DocumentProcessingQueue>()), Times.Exactly(10));
		}
	}
}
