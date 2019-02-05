namespace Fti.Labs.Core.Services
{
	using Fti.Labs.Core.Extensions;
	using Fti.Labs.Core.Interfaces;
	using Fti.Labs.Core.Interfaces.Data;
	using Serilog;

	public static class LoggerFactory
	{
		public static ILogger CreateAgentLogger(IAgent agent, ILogRepository logRepository) =>
			new LoggerConfiguration()
			.WithAgentLogging(agent)
			.WithDatabaseLogging(logRepository)
			.CreateLogger();

		public static ILogger CreateEventHandlerLogger(ILogRepository logRepository) =>
			new LoggerConfiguration()
				.WithDatabaseLogging(logRepository)
				.CreateLogger();

		public static ILogger CreateDefaultLogger() =>
			new LoggerConfiguration()
				.CreateLogger();

		public static LoggerConfiguration WithAgentLogging(this LoggerConfiguration config, IAgent agent) =>
			config.WriteTo.AgentLog(agent);

		public static LoggerConfiguration WithDatabaseLogging(this LoggerConfiguration config, ILogRepository logRepository) =>
			config.WriteTo.DatabaseLog(logRepository);
	}
}
