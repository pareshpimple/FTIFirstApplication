namespace Fti.Labs.Core.Interfaces.Data
{
	using kCura.Relativity.Client.DTOs;
	using System.Collections.Generic;

	public interface IDocumentRepository
	{
		Document Read(int workspaceId, int artifactId);

		IList<Document> Search(int workspaceId, string savedSearchName);

		IList<Document> Search(int workspaceId, int searchArtifactId);
	}
}
