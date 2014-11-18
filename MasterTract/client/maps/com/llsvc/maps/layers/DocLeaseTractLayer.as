package com.llsvc.maps.layers
{
	public class DocLeaseTractLayer extends BaseLayer
	{
		public function DocLeaseTractLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Leases";
			layerName = "All tracts";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:doc_lease_tract_view";
		}
	}
}