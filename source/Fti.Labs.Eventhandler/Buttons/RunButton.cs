using System;
using System.Collections.Generic;
using Fti.Labs.Core.Constants;
using Fti.Labs.Core.Interfaces.Data;
using Fti.Labs.Core.Models;
using Fti.Labs.Core.Services;
using Fti.Labs.Core.Services.EventHandler;
using Fti.Labs.EventHandler.Console;
using kCura.EventHandler;
using Relativity.API;
using Serilog;
using StructureMap;

namespace Fti.Labs.EventHandler.Buttons
{
	public class RunButton : BaseButton
	{
		public RunButton(BaseConsole console)
			: base(console, new ButtonInfo { Name = "runButton", DisplayText = "Run", ToolTip = "Start the job" })
		{
		}

		protected override bool IsButtonEnabled()
		{
            // Check status of the job
            throw new NotImplementedException();
        }

        public override void ServerOnClick()
        {
            using (var container = new Container(_ =>
            {
                _.For<IHelper>().Use(this.Console.Helper);
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssembliesFromApplicationBaseDirectory();
                    x.WithDefaultConventions();
                });
                _.For<ILogger>().Use(c =>
                    LoggerFactory.CreateEventHandlerLogger(c.TryGetInstance<ILogRepository>()));
            }))
            {
                throw new NotImplementedException();
            }
        }

        public override IList<Field> RequiredFields =>
            new List<Field>
            {
                new Field(Guids.Fields.Job.Status)
            };
    }
}
