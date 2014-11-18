package com.llsvc.maps.layers
{
import flash.events.Event;
import flash.events.HTTPStatusEvent;
import flash.events.IOErrorEvent;
import flash.events.SecurityErrorEvent;

import mx.containers.Canvas;
import mx.controls.Image;

[Bindable]
public class BaseLayer extends Canvas
{
	public var layerGroup:String;
	public var layerName:String;
	
	public var currentUrl:String;
	
	public var isLoading:Boolean = false;
	public var isLoaded:Boolean = false;
	
	public var isSelected:Boolean = false;
	
	protected var geoServerUrl:String;
	
	public var minZoomLevel:int;
	public var maxZoomLevel:int;
	
	public var currentZoomLevel:int;
	
	public function BaseLayer(geoServUrl:String)
	{
		super();
		
		this.horizontalScrollPolicy = "off";
		this.verticalScrollPolicy = "off";
		
		this.percentHeight = 100;
		this.percentWidth = 100;
		
		this.minZoomLevel = 1;
		this.maxZoomLevel = 16;
		
		this.geoServerUrl = geoServUrl;

		validateSize();
	}
	
	public function load(zoomLevel:int, minLon:Number, minLat:Number, maxLon:Number, maxLat:Number):void 
	{
        var bbs:String = minLon.toString() + "," + minLat.toString() + "," + maxLon.toString() + "," + maxLat.toString();
        var url:String = geoServerUrl + "wms?" + 
            "bbox=" + bbs +
            "&Format=image/png" +
            "&request=GetMap" + 
            "&layers=" + getGeoServerLayer() +
            "&styles=" +
            "&width=" + this.width + 
            "&height=" + this.height +
            "&transparent=false" +
            "&srs=EPSG:4267";
		
		if (url == currentUrl && (isLoaded || isLoading))
			return;

		this.removeAllChildren();
		
		currentUrl = url;
		currentZoomLevel = zoomLevel;
        
		if (currentZoomLevel < minZoomLevel || currentZoomLevel > maxZoomLevel)
			return;

        var image:Image = new Image();
        image.addEventListener(Event.COMPLETE, imageLoadHandler);
        image.addEventListener(IOErrorEvent.IO_ERROR, ioErrorHandler);
        image.addEventListener(SecurityErrorEvent.SECURITY_ERROR, securityErrorHandler);
        image.addEventListener(HTTPStatusEvent.HTTP_STATUS, httpStatusHandler);

		isLoading = true;
		isLoaded = false;
		
        image.load(url);
	}
	
	public function reload():void 
	{
        var image:Image = new Image();
        image.addEventListener(Event.COMPLETE, imageLoadHandler);
        image.addEventListener(IOErrorEvent.IO_ERROR, ioErrorHandler);
        image.addEventListener(SecurityErrorEvent.SECURITY_ERROR, securityErrorHandler);
        image.addEventListener(HTTPStatusEvent.HTTP_STATUS, httpStatusHandler); 

		isLoading = true;
		isLoaded = false;
		
        image.load(currentUrl);
	}
	
	private function imageLoadHandler(event:Event):void 
	{
		isLoading = false;
		isLoaded = true;
		
		var image:Image = event.target as Image;
		this.removeAllChildren();
		this.addChild(image);
		
		dispatchEvent(new Event("layer_loaded"));
	}
	
	private function ioErrorHandler(event:IOErrorEvent):void 
	{
		isLoading = false;
		isLoaded = false;
		
		trace ("BaseLayer.ioErrorHandler: " + event.text);

		dispatchEvent(new Event("layer_load_ioerror"));
	}
	
	private function securityErrorHandler(event:SecurityErrorEvent):void 
	{
		isLoading = false;
		isLoaded = false;
		
		trace ("BaseLayer.securityErrorHandler: " + event.text);

		dispatchEvent(new Event("layer_load_security_error"));
	}
	
	private function httpStatusHandler(event:HTTPStatusEvent):void 
	{
		isLoading = false;
		isLoaded = false;
		
		trace ("BaseLayer.httpStatusHandler: " + event.type);

		dispatchEvent(new Event("layer_load_http_status"));
	}
	
	protected function getGeoServerLayer():String 
	{
		throw new Error("You cannot use BaseLayer class. Need to override it.");
	}
	
}
}