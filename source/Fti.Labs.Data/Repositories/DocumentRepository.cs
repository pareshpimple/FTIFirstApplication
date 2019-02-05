namespace Fti.Labs.Data.Repositories
{
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Data.Extensions;
	using kCura.Relativity.Client;
	using kCura.Relativity.Client.DTOs;
	using Relativity.API;
	using System.Collections.Generic;
	using System.Linq;

	public class DocumentRepository : IDocumentRepository
	{
		private readonly IHelper helper;

		public DocumentRepository(IHelper helper)
		{
			this.helper = helper;
		}

		public Document Read(int workspaceId, int artifactId)
		{
			using (var rsapiClient = this.helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
			{
				rsapiClient.APIOptions.WorkspaceID = workspaceId;

				// Execute Query
				var result = rsapiClient.Repositories.Document.Read(artifactId);
				result.ThrowIfUnsuccessful($"Failed to read Document {artifactId} in workspace {workspaceId}");

				return result.Results.Single().Artifact;
			}
		}

		public IList<Document> Search(int workspaceId, string savedSearchName)
		{
			using (var rsapiClient = this.helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
			{
				rsapiClient.APIOptions.WorkspaceID = workspaceId;

				var searchArtifactId = ReadSavedSearchId(rsapiClient, workspaceId, savedSearchName);

				// Query the documents with a SavedSearchCondition.
				// Set the SelectedFields directive to retrieve the fields defined by the Saved Search.
				var docQuery = new Query<Document>
				{
					Condition = new SavedSearchCondition(searchArtifactId),
					Fields = FieldValue.SelectedFields
				};

				var result = rsapiClient.Repositories.Document.Query(docQuery);

				result.ThrowIfUnsuccessful($"Failed to read documents from saved search {searchArtifactId} in workspace {workspaceId}");

				return result.Results.Select(r => r.Artifact).ToList();
			}
		}

		public IList<Document> Search(int workspaceId, int searchArtifactId)
		{
			using (var rsapiClient = this.helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
			{
				rsapiClient.APIOptions.WorkspaceID = workspaceId;

				// Query the documents with a SavedSearchCondition.
				// Set the SelectedFields directive to retrieve the fields defined by the Saved Search.
				var docQuery = new Query<Document>
				{
					Condition = new SavedSearchCondition(searchArtifactId),
					Fields = FieldValue.SelectedFields
				};

				var result = rsapiClient.Repositories.Document.Query(docQuery);

				result.ThrowIfUnsuccessful($"Failed to read documents from saved search {searchArtifactId} in workspace {workspaceId}");

				return result.Results.Select(r => r.Artifact).ToList();
			}
		}

		internal static int ReadSavedSearchId(IRSAPIClient rsapiClient, int workspaceId, string savedSearch)
		{
			// Query the saved search artifact id from the saved search name
			var ssQuery = new Query
			{
				ArtifactTypeID = (int)ArtifactType.Search,
				Condition = new TextCondition("Name", TextConditionEnum.Like, savedSearch)
			};

			var savedSearchResult = rsapiClient.Query(rsapiClient.APIOptions, ssQuery);
			savedSearchResult.ThrowIfUnsuccessful($"Failed to read saved search `{savedSearch}` in workspace {workspaceId}");

			return savedSearchResult.QueryArtifacts[0].ArtifactID;
		}
	}
}
