namespace Fti.Labs.Core.Models
{
	public class DocumentProcessingQueue
	{
		public int Id { get; set; }

		public int WorkspaceId { get; set; }

		public int DocumentId { get; set; }

		public int Status { get; set; }
	}
}
