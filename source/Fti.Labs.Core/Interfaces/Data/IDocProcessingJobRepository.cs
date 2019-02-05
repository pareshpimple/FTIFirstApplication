using Fti.Labs.Core.Models;

namespace Fti.Labs.Core.Interfaces.Data
{
	public interface IDocProcessingJobRepository
	{
		DocumentProcessingJob ReadNextPendingJob();
	}
}
