namespace Fti.Labs.Core.Services.Logging
{
	using System;
	using Fti.Labs.Core.Extensions;
	using Fti.Labs.Core.Interfaces;
	using Serilog.Core;
	using Serilog.Events;

	public class AgentLogSink : ILogEventSink
	{
		private readonly IFormatProvider formatProvider;
		private readonly IAgent agent;

		public AgentLogSink(IAgent agent, IFormatProvider formatProvider)
		{
			this.formatProvider = formatProvider;
			this.agent = agent;
		}

		public void Emit(LogEvent logEvent)
		{
			var message = logEvent.RenderMessage(formatProvider).Truncate(30000);

			if (logEvent.Level == LogEventLevel.Error || logEvent.Level == LogEventLevel.Fatal)
			{
				this.agent.RaiseError(message, string.Empty);
			}
			else if (logEvent.Level == LogEventLevel.Warning)
			{
				this.agent.RaiseWarning(message, string.Empty);
			}
			else
			{
				this.agent.RaiseMessage(message, 10);
			}
		}
	}
}
