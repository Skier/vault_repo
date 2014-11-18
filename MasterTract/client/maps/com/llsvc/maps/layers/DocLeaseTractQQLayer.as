package com.llsvc.maps.layers
{
	public class DocLeaseTractQQLayer extends BaseLayer
	{
		public function DocLeaseTractQQLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Leases";
			layerName = "Tract QQs";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:doc_lease_tract_qq_view";
		} 
	}
}