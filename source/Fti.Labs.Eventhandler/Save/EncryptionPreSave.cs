using Fti.Labs.Core.Constants;
using kCura.EventHandler;
using kCura.EventHandler.CustomAttributes;
using System;
using System.Runtime.InteropServices;

namespace Fti.Labs.EventHandler.Save
{
	[Description("Encrypt email pre-save handler")]
	[Guid(Guids.EventHandlers.EncryptionPreSaveString)]
	public class EncryptionPreSave : PreSaveEventHandler
	{


		public override Response Execute()
		{
			try
			{
                throw new NotImplementedException();
            }
			catch (Exception ex)
			{
				return new Response { Success = false, Message = "Could not encrypt field `Email`", Exception = ex };
			}

			return new Response { Success = true, Message = string.Empty };
		}

		// The ActiveArtifact.Fields collection includes the fields returned by the RequiredFields property, and those on the current layout.
		// It also includes the values of these fields.
		public override FieldCollection RequiredFields
		{
			get
			{
				var retVal = new FieldCollection();
				retVal.Add(new Field(Guids.Fields.Document.Email));
				return retVal;
			}
		}
	}
}
