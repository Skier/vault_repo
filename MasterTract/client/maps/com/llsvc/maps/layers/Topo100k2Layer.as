package com.llsvc.maps.layers
{
	public class Topo100k2Layer extends BaseLayer
	{
		public function Topo100k2Layer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Topo";
			layerName = "100k2";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:100k2";
		} 
	}
}