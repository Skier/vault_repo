package com.llsvc.maps
{
	import mx.collections.ArrayCollection;
	
	public interface IBaseMap
	{
		function getLayers():ArrayCollection;
		function addLayer(value:IBaseLayer):void;
		function removeLayer(value:IBaseLayer):void;
		function removeLayerAt(index:int):void;
		
		function loadAll():void ;
		function loadLayer(layer:IBaseLayer):void ;
	}
}