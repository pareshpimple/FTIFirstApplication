using Fti.Labs.Core.Models;
using kCura.EventHandler;

namespace Fti.Labs.EventHandler.Buttons
{
	public class SeparatorConsoleButton : BaseButton
	{
		public SeparatorConsoleButton()
			: base(new ButtonInfo { Name = "ConsoleSeparator", DisplayText = string.Empty, ToolTip = string.Empty })
		{

		}
		protected override bool IsButtonEnabled() => true;

		public override IConsoleItem ToConsoleItem() => new ConsoleSeparator();
	}
}
