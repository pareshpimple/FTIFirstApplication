namespace Fti.Labs.Core.Services.Logging
{
	using System;
	using Fti.Labs.Core.Extensions;
	using Fti.Labs.Core.Interfaces.Data;
	using Fti.Labs.Core.Models;
	using Serilog.Core;
	using Serilog.Events;

	public class DatabaseLogSink : ILogEventSink
	{
		private readonly IFormatProvider _formatProvider;
		private readonly ILogRepository logRepository;

		public DatabaseLogSink(ILogRepository logRepository, IFormatProvider formatProvider)
		{
			_formatProvider = formatProvider;
			this.logRepository = logRepository;
		}

		public void Emit(LogEvent logEvent)
		{
			var message = logEvent.RenderMessage(_formatProvider).Truncate(30000);

			this.logRepository.Create(new LogEntry { Level = logEvent.Level.ToString(), Message = message });
		}
	}
}
