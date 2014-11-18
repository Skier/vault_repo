package com.fxcomponents.controls.fxvideo
{
public class VolumeButton extends Button
{
	public function VolumeButton()
	{
		super();
	}
	
	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	{
		super.updateDisplayList(unscaledWidth, unscaledHeight);
		
		icon.graphics.clear();
		icon.graphics.beginFill(iconColor);
		icon.graphics.drawRect(0, 2, 2, 5);
		icon.graphics.drawRect(3, 2, 1, 5);
		icon.graphics.drawRect(4, 1, 1, 7);
		icon.graphics.drawRect(5, 0, 1, 9);
		
		centerIcon();
	}
}
}