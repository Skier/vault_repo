package com.llsvc.maps.layers
{
	public class TobinQQLayer extends BaseLayer
	{
		public function TobinQQLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Tobin";
			layerName = "QQ";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:tobin_qq";
		} 

	}
}