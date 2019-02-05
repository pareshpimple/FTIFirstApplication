using System;
using System.Runtime.InteropServices;
using Fti.Labs.Core.Constants;
using kCura.EventHandler;
using kCura.EventHandler.CustomAttributes;

namespace Fti.Labs.EventHandler.PageInteraction
{
	[Guid("0CCF8E3C-4AB0-4C48-BC2C-7933CA7496C6")]
	[Description("Job Button Event Handler")]
	public class JobPageInteraction : PageInteractionEventHandler
	{
		public override Response PopulateScriptBlocks()
		{
			try
			{
				this.PopulateExportButton(this);
			}
			catch(Exception ex)
			{
				return new Response { Success = false, Message = ex.ToString(), Exception = ex };
			}

			return new Response { Success = true, Message = string.Empty };
		}

		private void PopulateExportButton(PageInteractionEventHandler handler)
		{
			var customPagePath =
				handler.Helper.GetUrlHelper().GetRelativePathToCustomPages(Guids.Application.ApplicationGuid);

			var exportUrl = $"{customPagePath}/api/Job/Export?workspaceId={this.Helper.GetActiveCaseID()}&artifactId={this.ActiveArtifact.ArtifactID}";

            // Adds a script block (ie <script>...</script>) to the page
            // This adds the url for our web service with the relevant ids
            throw new NotImplementedException();

			// Adds a javascript file to the page
			// This would add the javascript to display a button that utilizes the url above
			// For this to work you'd need to have custom pages added to the application with the file below. In this example that hasn't been added yet.
		}
	}
}
