package com.llsvc.maps.layers
{
	public class TobinTwnLayer extends BaseLayer
	{
		public function TobinTwnLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Tobin";
			layerName = "Township";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:tobin_twn";
		} 
	}
}