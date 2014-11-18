package com.loveland.mapper.tracts
{
	import com.afcomponents.umap.overlays.Polyline;
	import com.afcomponents.umap.types.LatLng;

	public class TemporaryLine extends Polyline
	{
		override public function TemporaryLine(startPoint:LatLng, endPoint:LatLng, style:Object=null)
		{
			var param:Object = new Object();
			param.points = new Array();
			param.points.push(startPoint);
			param.points.push(endPoint);
			
			super(param, style);
			
			autoInfo = false;
		}
		
		public function get startPoint():LatLng 
		{
			if (points.length > 0) 
				return points[0] as LatLng;
			else 
				return null;
		}
		
		public function set startPoint(value:LatLng):void  
		{
			if (points == null)
				points = new Array();
				
			if (points.length > 0) 
				points[0] = value;
			else 
				points.push(value);
			
			refresh(true);
		}
		
		public function get endPoint():LatLng 
		{
			if (points.length > 1) 
				return points[1] as LatLng;
			else 
				return null;
		}
		
		public function set endPoint(value:LatLng):void  
		{
			if (points == null)
				points = new Array();
				
			if (points.length > 1) 
				points[1] = value;
			else if (points.length > 0)
				points.push(value);
			else 
			{
				points.push(new LatLng(value.lat, value.lng));
				points.push(value);
			}
			
			refresh(true);
		}
		
		public function move(startPoint:LatLng, endPoint:LatLng):void 
		{
			points = new Array();
			points.push(startPoint);
			points.push(endPoint);
			
			super.refresh(true);
		}
	}
}