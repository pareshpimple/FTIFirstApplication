namespace Fti.Labs.Data.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Data.Extensions;
	using kCura.Relativity.Client;
	using kCura.Relativity.Client.DTOs;
	using Relativity.API;

	public class RdoRepository : IRdoRepository
	{
		private readonly IHelper helper;

		public RdoRepository(IHelper helper)
		{
			this.helper = helper;
		}

		public RDO Read(int workspaceId, int artifactId, IList<string> fields, int artifactTypeId)
		{
			using (var rsapiClient = this.helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
			{
				rsapiClient.APIOptions.WorkspaceID = workspaceId;
				// Build Query
				var rdoQuery = new Query<RDO>
				{
					ArtifactTypeID = artifactTypeId,
					Fields = fields.Select(f => new FieldValue(f)).ToList(),
					Condition = new kCura.Relativity.Client.WholeNumberCondition(
						ArtifactQueryFieldNames.ArtifactID,
						NumericConditionEnum.EqualTo,
						artifactId)
				};

				// Execute Query
				var result = rsapiClient.Repositories.RDO.Query(rdoQuery);
				result.ThrowIfUnsuccessful("Failed to read RDO");

				return result.Results.Single().Artifact;
			}
		}

		public IList<RDO> ReadAll(int workspaceId, int artifactTypeId)
		{
			using (var rsapiClient = this.helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
			{
				rsapiClient.APIOptions.WorkspaceID = workspaceId;
				var query = new Query<RDO>
				{
					ArtifactTypeID = artifactTypeId,
					Fields = FieldValue.AllFields
				};
				var resultSet = rsapiClient.Repositories.RDO.Query(query);
				resultSet.ThrowIfUnsuccessful("Failed to read RDO");

				return resultSet.Results.Select(r => r.Artifact).ToList();
			}
		}

		public int ReadArtifactTypeId(int workspaceId, Guid artifactTypeGuid)
		{
			using (var rsapiClient = this.helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
			{
				rsapiClient.APIOptions.WorkspaceID = workspaceId;

				var result = rsapiClient.Repositories.ObjectType.ReadSingle(artifactTypeGuid);
				return result.DescriptorArtifactTypeID.Value;
			}
		}
	}
}
