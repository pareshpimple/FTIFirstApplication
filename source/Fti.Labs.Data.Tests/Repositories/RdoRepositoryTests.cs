namespace Fti.Labs.Data.Tests.Repositories
{
	using System;
	using Fti.Labs.Data.Repositories;
	using Fti.Labs.Data.Tests.Properties;
	using kCura.Relativity.Client;
	using Moq;
	using NUnit.Framework;
	using Relativity.API;

	[TestFixture]
	public class RdoRepositoryTests
	{
		[SetUp]
		public void Setup()
		{
			this.helper = new Mock<IHelper>();
			this.serviceMgr = new Mock<IServicesMgr>();
			this.helper.Setup(h => h.GetServicesManager()).Returns(this.serviceMgr.Object);
			this.serviceMgr.Setup(s => s.CreateProxy<IRSAPIClient>(It.IsAny<ExecutionIdentity>()))
				.Returns(new RSAPIClient(
							new Uri(Resources.RsapiUrl),
							new UsernamePasswordCredentials(Resources.RelativityUsername, Resources.RelativityUsername),
							new RSAPIClientSettings { CertificateValidation = false }));
			this.rdoRepository = new RdoRepository(this.helper.Object);
		}

		private Mock<IHelper> helper;
		private Mock<IServicesMgr> serviceMgr;
		private RdoRepository rdoRepository;

		[Test]
		public void Read()
		{
			// Arrange

			// Act
			var allRdos = this.rdoRepository.ReadAll(123, 123);

			// Assert
		}
	}
}
