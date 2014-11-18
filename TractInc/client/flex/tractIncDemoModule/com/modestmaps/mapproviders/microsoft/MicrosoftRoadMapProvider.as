package com.modestmaps.mapproviders.microsoft
{
	import com.modestmaps.core.Coordinate;
	import com.modestmaps.mapproviders.IMapProvider;
	import com.modestmaps.mapproviders.microsoft.AbstractMicrosoftMapProvider;
	
	
	/**
	 * @author darren
	 * $Id: MicrosoftRoadMapProvider.as 350 2007-10-20 01:16:06Z migurski $
	 */
	
	public class MicrosoftRoadMapProvider 
		extends AbstractMicrosoftMapProvider
		implements IMapProvider
	{
		override public function toString():String
		{
			return "MICROSOFT_ROAD";
		}
		
		override public function getTileUrl(coord:Coordinate):String
		{		
	        return "http://r" + Math.floor(Math.random() * 4) + ".ortho.tiles.virtualearth.net/tiles/r" + getZoomString( coord ) + ".png?g=90&shading=hill";
		}
	}
}