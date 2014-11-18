package sjd.controls
{
	import mx.controls.ToolTip;

	/**
	 * @class HtmlToolTip
	 * @brief A ToolTip that supports html text.
	 * @author Jove
	 * @version 1.2
	 */
	public class HtmlToolTip extends ToolTip
	{
		override protected function commitProperties():void
		{
			super.commitProperties();
	
			textField.htmlText = text
		}
	}
}