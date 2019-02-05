using Fti.Labs.Core.Extensions;
using Fti.Labs.EventHandler.Buttons;
using kCura.EventHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using Field = kCura.EventHandler.Field;

namespace Fti.Labs.EventHandler.Console
{
	/// <summary>
	/// A wrapper inheriting class for defining a console event handler as a set of buttons
	/// </summary>
	public abstract class BaseConsole : ConsoleEventHandler
	{
		/// <summary>
		/// Gets the fields required for the console or any of its buttons.
		/// The ActiveArtifact.Fields collection includes the fields returned by the RequiredFields property, and those on the current layout.
		/// It also includes the values of these fields.
		/// </summary>
		public sealed override FieldCollection RequiredFields
		{
			get
			{
				var fieldCollection = new FieldCollection();
				Buttons
					.SelectMany(x => x.RequiredFields)		// Select all the required fields on each button
					.Concat(ExtraRequiredFields)			// Add the fields fields required by the console itself
					.Where(f => f != null)					// Make sure none of the fields are null
					.DistinctBy(f=>f.Name)					// Filter out duplicates
					.ForEach(f => fieldCollection.Add(f));	// Added them to the field collection

				return fieldCollection;
			}
		}

		protected abstract string ConsoleTitle { get; }

		/// <summary>
		/// Gets or sets the fields required by the console itself (as opposed to its buttons), e.g. for
		/// determining visibility.
		/// </summary>
		protected virtual IList<Field> ExtraRequiredFields { get; set; } = new List<Field>();

		/// <summary>
		/// Gets the buttons this console should have
		/// </summary>
		protected abstract IList<BaseButton> Buttons { get; }

		/// <summary>
		/// Gets the console defined by the specified buttons
		/// </summary>
		/// <param name="pageEvent">The page event.</param>
		/// <returns>The console.</returns>
		public override kCura.EventHandler.Console GetConsole(PageEvent pageEvent) =>
			new kCura.EventHandler.Console
			{
				Title = ConsoleTitle,
				Items = Buttons.Select(x => x.ToConsoleItem()).ToList()
			};

		/// <summary>
		/// Executes the appropriate action for the clicked button.
		/// </summary>
		/// <param name="consoleButton">The button that was clicked.</param>
		public sealed override void OnButtonClick(ConsoleButton consoleButton)
		{
			if (!consoleButton.RaisesPostBack)
			{
				return; // client-side event
			}

			var button = Buttons.Single(x => x.Button.Name == consoleButton.Name);

			try
			{
				button.ServerOnClick();
			}
			catch (kCura.Relativity.Client.APIException ex)
			{
				// Api Exception doesn't bubble up well
				throw new Exception(ex.ToString());
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}
	}
}
