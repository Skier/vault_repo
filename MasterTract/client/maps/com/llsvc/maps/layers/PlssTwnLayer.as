package com.llsvc.maps.layers
{
	public class PlssTwnLayer extends BaseLayer
	{
		public function PlssTwnLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "PLSS";
			layerName = "Township";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:plss_twn";
		} 
	}
}