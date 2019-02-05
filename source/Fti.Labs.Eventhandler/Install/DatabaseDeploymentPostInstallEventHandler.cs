using System;
using Fti.Labs.Core.Constants;
using Fti.Labs.Core.Interfaces.Data;
using Fti.Labs.Core.Services;
using Fti.Labs.Data.Services;
using kCura.EventHandler;
using kCura.EventHandler.CustomAttributes;
using Relativity.API;
using Serilog;
using StructureMap;

namespace Fti.Labs.EventHandler.Install
{
	[kCura.EventHandler.CustomAttributes.RunOnce(false)]
	[kCura.EventHandler.CustomAttributes.Description("Deployment and updates for database tables during installation")]
	[System.Runtime.InteropServices.Guid(Guids.EventHandlers.PostInstallEventHanlder)]
	[RunTarget(kCura.EventHandler.Helper.RunTargets.InstanceAndWorkspace)]
	public class DatabaseDeploymentPostInstallEventHandler : PostInstallEventHandler
	{
		public override Response Execute()
		{
			try
			{
                using (var container = new Container(_ =>
                {
                    _.For<IHelper>().Use(this.Helper);
                    _.Scan(x =>
                    {
                        x.TheCallingAssembly();
                        x.WithDefaultConventions();
                    });
                    _.For<ILogger>().Use(c =>
                        LoggerFactory.CreateEventHandlerLogger(c.TryGetInstance<ILogRepository>()));
                }))
                {
                    // Start the agent implementation class.
                    throw new NotImplementedException();
                }
            }
			catch(Exception ex)
			{
				return new Response { Message = $"Failed to deploy and update database tables. {ex}", Success = false };
			}

            return new Response { Message = $"Successfully deployed and updated database tables.", Success = true };
        }
	}
}
