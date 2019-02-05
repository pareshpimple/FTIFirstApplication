namespace Fti.Labs.Core.Interfaces
{
	using System.Threading.Tasks;
	using kCura.Relativity.Client.DTOs;

	public interface IDocumentProcessingService
	{
		Task ProcessDocument(Document document);
	}
}
