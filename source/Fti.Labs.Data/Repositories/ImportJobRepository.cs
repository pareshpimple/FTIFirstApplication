namespace Fti.Labs.Data.Repositories
{
	using System;
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Core.Models;

	public class ImportJobRepository
	{
		private readonly IRdoRepository rdoRepository;

		public ImportJobRepository(IRdoRepository rdoRepository)
		{
			this.rdoRepository = rdoRepository;
		}

		public DocumentProcessingJob ReadNextJob()
		{
			throw new NotImplementedException();
		}
	}
}
