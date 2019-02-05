using Fti.Labs.Core.Models;
using Fti.Labs.Data.Repositories;
using Moq;
using NUnit.Framework;
using Relativity.API;

namespace Fti.Labs.Data.Tests.Repositories
{
	[TestFixture]
	public class DocumentQueueRepositoryTests
	{
		[OneTimeSetUp]
		public void Setup()
		{
			var helper = new Mock<IHelper>();
			helper.Setup(h => h.GetDBContext(It.IsAny<int>()))
				.Returns(new RemoteDbContext(ConnectionSetup.GetConnection()));
			ConnectionSetup.DeployDatabase(helper.Object);
			documentQueueRepository = new DocumentQueueRepository(helper.Object);
		}

		private DocumentQueueRepository documentQueueRepository;

		[Test]
		public void DocumentQueue_Create()
		{
			// Arrange

			// Act
			this.documentQueueRepository.Create(new DocumentProcessingQueue { DocumentId = 123, WorkspaceId = 555 });

			// Assert
		}

		[Test]
		public void DocumentQueue_ReadNext()
		{
			// Arrange
			this.documentQueueRepository.Create(new DocumentProcessingQueue { DocumentId = 333, WorkspaceId = 765 });

			// Act
			var result = this.documentQueueRepository.ReadNextFromQueue();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.GreaterThan(0));
			Assert.That(result.DocumentId, Is.GreaterThan(0));
			Assert.That(result.WorkspaceId, Is.GreaterThan(0));
			Assert.That(result.Status, Is.EqualTo(1));
		}
	}
}
