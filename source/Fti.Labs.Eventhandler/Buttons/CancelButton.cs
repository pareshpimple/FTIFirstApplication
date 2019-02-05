using Fti.Labs.Core.Constants;
using Fti.Labs.Core.Interfaces.Data;
using Fti.Labs.Core.Models;
using Fti.Labs.Core.Services;
using Fti.Labs.Core.Services.EventHandler;
using Fti.Labs.Data.Repositories;
using Fti.Labs.EventHandler.Console;
using kCura.EventHandler;
using Relativity.API;
using Serilog;
using StructureMap;
using System;
using System.Collections.Generic;

namespace Fti.Labs.EventHandler.Buttons
{
	public class CancelButton : BaseButton
	{
		public CancelButton(BaseConsole console)
			: base(console, new ButtonInfo{ Name = "cancelButton", DisplayText = "Cancel", ToolTip = " Cancel the job"})
		{
		}

		protected override bool IsButtonEnabled()
		{
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
            new []
            {
                new Field(Guids.Fields.Job.Status)
            };
    }
}
