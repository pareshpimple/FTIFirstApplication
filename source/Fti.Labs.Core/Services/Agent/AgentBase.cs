namespace Fti.Labs.Core.Services.Agent
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Fti.Labs.Core.Extensions;
	using Fti.Labs.Core.Interfaces;
	using Serilog;

	public abstract class AgentBase : IAgentImpl
	{
		private readonly IAgent agent;
		private readonly ILogger logger;
		private CancellationTokenSource cancellationTokenSource;

		protected AgentBase(IAgent agent, ILogger logger)
		{
			this.agent = agent;
			this.logger = logger;
			this.agent.OnFtiAgentDisabled += AgentDisabled;
		}


		internal abstract Task ExecuteFtiAgent(CancellationToken token);

		public void Execute()
		{
			// Signal that the agent has started work. This shows message is displayed in the Relativity Agents Tab
			this.agent.RaiseMessage($"Started {this.agent.Name}.", 10);

			// Inner try-catch to handle most application exceptions
			try
			{
				// Create a new cancellation token.
				using (var source = new CancellationTokenSource())
				{
					this.cancellationTokenSource = source;

					// Start the agent workflow
					this.ExecuteFtiAgent(cancellationTokenSource.Token)
						.GetAwaiter().GetResult();
				}

				// Signal that the agent has completed work. This shows message is displayed in the Relativity Agents Tab
				this.agent.RaiseMessage($"Completed {this.agent.Name}.", 10);
			}
			catch (Exception ex)
			{
				// Log error to any custom logs.
				this.logger.Error(ex, $"Error executing {this.agent.Name}. {ex.Message}");

				// Raise error message. This shows message is displayed in the Relativity Agents Tab
				var errorMessage = $"Error executing {this.agent.Name}. {ex}".Truncate(30000);
				this.agent.RaiseError(errorMessage, string.Empty);
			}
		}

		private void AgentDisabled(object sender, EventArgs e)
		{
			this.cancellationTokenSource?.Cancel();
		}
	}
}
