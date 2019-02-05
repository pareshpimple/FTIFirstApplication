using System.Collections.Generic;
using Fti.Labs.Core.Models;
using Fti.Labs.EventHandler.Console;
using kCura.EventHandler;

namespace Fti.Labs.EventHandler.Buttons
{
	/// <summary>
	/// A base class for defining console button properties and logic.
	/// </summary>
	public abstract class BaseButton
	{
		public BaseButton(BaseConsole console, ButtonInfo button)
			: this(button)
		{
			this.Console = console;
		}

		public BaseButton(ButtonInfo button)
		{
			this.Button = button;
		}

		protected BaseConsole Console { get; private set; }

		public ButtonInfo Button { get; private set; }

		/// <summary>
		/// Gets or sets the fields required for this button to work properly.
		/// </summary>
		public virtual IList<Field> RequiredFields { get; protected set; } = new List<Field>();

		/// <summary>
		/// Gets or sets the Javascript to execute on the client when clicked (leave null for server-side).
		/// </summary>
		public virtual string ClientOnClickJs { get; protected set; }

		/// <summary>
		/// The code to execute server-side if this is a server-side button.
		/// </summary>
		public virtual void ServerOnClick()
		{
			// does nothing unless overriden
		}

		/// <summary>
		/// Turns the button into a Relativity console button.
		/// </summary>
		/// <returns>A Relativity console button</returns>
		public virtual ConsoleButton ToConsoleButton() =>
			new ConsoleButton
			{
				Name = this.Button.Name,
				DisplayText = this.Button.DisplayText,
				ToolTip = this.Button.ToolTip,
				RaisesPostBack = string.IsNullOrWhiteSpace(this.ClientOnClickJs),
				Enabled = this.IsButtonEnabled(),
				OnClickEvent = this.ClientOnClickJs,
			};

		public virtual IConsoleItem ToConsoleItem() => this.ToConsoleButton();

		/// <summary>
		/// Determines whether the button should be enabled.
		/// </summary>
		/// <returns>Whether the button should be enabled</returns>
		protected abstract bool IsButtonEnabled();
	}
}
