namespace Fti.Labs.Core.Interfaces.Data
{
	using System;
	using System.Collections.Generic;
	using kCura.Relativity.Client.DTOs;

	public interface IWorkspaceRepository
	{
		IList<Workspace> ReadAllWithApplication(Guid applicationGuid);
	}
}
