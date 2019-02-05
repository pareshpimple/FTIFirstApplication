namespace Fti.Labs.Data.Tests.Services
{
	using Moq;
	using NUnit.Framework;
	using Relativity.API;

	[TestFixture]
	public class DatabaseDeploymentServiceTests
	{
		[SetUp]
		public void Setup()
		{
			this.helper = new Mock<IHelper>();
			this.helper.Setup(h => h.GetDBContext(It.IsAny<int>()))
				.Returns(new RemoteDbContext(ConnectionSetup.GetConnection()));
		}

		private Mock<IHelper> helper;

		[Test]
		public void DatabaseDeploymentService_DeployDatabase()
		{
			// Act
			ConnectionSetup.DeployDatabase(this.helper.Object);
		}
	}
}
