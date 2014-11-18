package com.llsvc.maps.layers
{
	public class TobinSecLayer extends BaseLayer
	{
		public function TobinSecLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Tobin";
			layerName = "Section";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:tobin_sec";
		} 

	}
}