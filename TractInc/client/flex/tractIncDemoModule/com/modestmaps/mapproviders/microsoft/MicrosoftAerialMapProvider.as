
package com.modestmaps.mapproviders.microsoft
{
	import com.modestmaps.core.Coordinate;
	import com.modestmaps.mapproviders.IMapProvider;
	import com.modestmaps.mapproviders.microsoft.AbstractMicrosoftMapProvider;
	
	/**
	 * @author darren
	 * $Id: MicrosoftAerialMapProvider.as 350 2007-10-20 01:16:06Z migurski $
	 */
	
	public class MicrosoftAerialMapProvider 
		extends AbstractMicrosoftMapProvider
		implements IMapProvider
	{
		override public function toString():String
		{
			return "MICROSOFT_AERIAL";
		}
		
		override public function getTileUrl(coord:Coordinate):String
		{		
	        return "http://a" + Math.floor(Math.random() * 4) + ".ortho.tiles.virtualearth.net/tiles/a" + getZoomString( coord ) + ".jpeg?g=90&shading=hill";
		}
	}
}