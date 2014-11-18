package com.llsvc.maps.layers
{
	public class OrphanLayer extends BaseLayer
	{
		public function OrphanLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Leases";
			layerName = "Orphan";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:doc_lease_tract_orphan_view";
		} 
	}
}