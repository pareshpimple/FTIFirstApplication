namespace Fti.Labs.Core.Models
{
	using System;

	public class LogEntry
	{
		public string Level { get; set; }

		public string Message { get; set; }

		public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
	}
}
