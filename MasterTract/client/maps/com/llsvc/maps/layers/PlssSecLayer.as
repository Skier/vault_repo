package com.llsvc.maps.layers
{
	public class PlssSecLayer extends BaseLayer
	{
		public function PlssSecLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "PLSS";
			layerName = "Section";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:plss_sec";
		} 
	}
}