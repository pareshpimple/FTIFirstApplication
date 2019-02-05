using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Fti.Labs.Core.Constants;
using Fti.Labs.EventHandler.Buttons;
using kCura.EventHandler.CustomAttributes;

namespace Fti.Labs.EventHandler.Console
{
	[Guid(Guids.EventHandlers.JobConsoleString)]
	[Description(Names.EventHandlers.JobConsole)]
	public class JobConsole : BaseConsole
	{
		protected override string ConsoleTitle { get; } = Names.EventHandlers.JobConsoleTitle;

        protected override IList<BaseButton> Buttons => throw new NotImplementedException();
	}
}
