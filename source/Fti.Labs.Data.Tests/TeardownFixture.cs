using NUnit.Framework;

namespace Fti.Labs.Data.Tests
{
	[SetUpFixture]
	public class TeardownFixture
	{
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			ConnectionSetup.DeleteDatabases();
		}
	}
}
