package com.llsvc.maps.layers
{
	public class DocLeaseTractIntersecLayer extends BaseLayer
	{
		public function DocLeaseTractIntersecLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Leases";
			layerName = "Intersect";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:doc_lease_tract_intersect_view";
		} 
	}
}