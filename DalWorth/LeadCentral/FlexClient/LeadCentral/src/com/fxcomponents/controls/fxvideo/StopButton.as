package com.fxcomponents.controls.fxvideo
{
public class StopButton extends Button
{
	public function StopButton()
	{
		super();
	}
	
	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	{
		super.updateDisplayList(unscaledWidth, unscaledHeight);
		
		icon.graphics.clear();
		icon.graphics.beginFill(iconColor);
		
		icon.graphics.drawRect(0, 0, 7, 7);
		
		centerIcon();
	}
}
}