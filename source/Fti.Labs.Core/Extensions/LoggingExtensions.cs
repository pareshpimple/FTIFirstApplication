namespace Fti.Labs.Core.Extensions
{
	using System;
	using Fti.Labs.Core.Interfaces;
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Core.Services.Logging;
	using Serilog;
	using Serilog.Configuration;

	public static class LoggingExtensions
	{
		public static LoggerConfiguration AgentLog(
			this LoggerSinkConfiguration loggerConfiguration,
			IAgent agent,
			IFormatProvider formatProvider = null)
		{
			return loggerConfiguration.Sink(new AgentLogSink(agent, formatProvider));
		}

		public static LoggerConfiguration DatabaseLog(
			this LoggerSinkConfiguration loggerConfiguration,
			ILogRepository logRepository,
			IFormatProvider formatProvider = null)
		{
			return loggerConfiguration.Sink(new DatabaseLogSink(logRepository, formatProvider));
		}
	}
}
