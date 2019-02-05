namespace Fti.Labs.Core.Interfaces.Data
{
	using System;
	using System.Collections.Generic;
	using kCura.Relativity.Client.DTOs;

	public interface IRdoRepository
	{
		RDO Read(int workspaceId, int artifactId, IList<string> fields, int artifactTypeId);

		IList<RDO> ReadAll(int workspaceId, int artifactTypeId);

		int ReadArtifactTypeId(int workspaceId, Guid artifactTypeGuid);
	}
}
