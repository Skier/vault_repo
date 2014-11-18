package com.ebs.eroof.mapping
{
	import com.afcomponents.umap.core.UMap;
	import com.afcomponents.umap.overlays.Layer;
	import com.afcomponents.umap.types.LatLng;
	
	import flash.events.IEventDispatcher;
	
	public interface IMapContainer extends IEventDispatcher
	{
		function getUMap():UMap;

		function getCurrentMousePosition():LatLng;

		function getMapZoom():Number;
		function setMapZoom(value:Number):void;

		function getMapCenter():LatLng;
		function setMapCenter(value:LatLng):void;

		function getMapType():String;
		function setMapType(value:String):void;

		function getMarkerLayer():Layer;
		function getPolygonLayer():Layer;
	}
}