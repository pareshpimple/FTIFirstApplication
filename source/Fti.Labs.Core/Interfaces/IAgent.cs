using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fti.Labs.Core.Interfaces
{
	public interface IAgent
	{
		int AgentID { get; }

		string Name { get; }

		int LoggingLevel { get; set; }

		void RaiseMessage(string message, int level);

		void RaiseWarning(string message);

		void RaiseWarning(string message, string detailMessage);

		void RaiseError(string message, string detailMessage);

		event EventHandler OnFtiAgentDisabled;
	}
}
