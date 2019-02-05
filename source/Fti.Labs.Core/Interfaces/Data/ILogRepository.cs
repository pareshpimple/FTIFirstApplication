namespace Fti.Labs.Core.Interfaces.Data
{
	using System.Threading.Tasks;
	using Fti.Labs.Core.Models;

	public interface ILogRepository
	{
		Task Create(LogEntry logEntry);
	}
}
