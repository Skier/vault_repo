package com.llsvc.maps.storage
{
import com.llsvc.domain.Layer;

import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.remoting.RemoteObject;

public class Storage 
    implements IStorage
{
    private static const SERVICE:String = "geoService";
    
    private static var _instance:Storage;

    public static function get instance():Storage
    {
        if (_instance == null) {
            _instance = new Storage();
        }
        return _instance;
    }
    
    private var service:RemoteObject = null;
    
    public function Storage()
    {
        if (_instance != null) {
            throw new Error("Use instance getter instead of constructor. It's singleton !");
        }
    }
    
    public function getSRSes(responder:Responder):void {
        var asyncToken:AsyncToken = getService().getSRSes();
        asyncToken.addResponder(responder);
    }
    
    public function getLayers(responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLayers();
        asyncToken.addResponder(responder);
    }
    
    public function saveLayer(layer:Layer, responder:Responder):void {
        var asyncToken:AsyncToken = getService().saveLayer(layer);
        asyncToken.addResponder(responder);
    }
    
    public function removeLayer(layerId:int, responder:Responder):void {
        var asyncToken:AsyncToken = getService().removeLayer(layerId);
        asyncToken.addResponder(responder);
    }
    
    private function getService():RemoteObject {
        if ( null == service ) {
            service = new RemoteObject();
            service.destination = Storage.SERVICE;
            service.source = Storage.SERVICE;
        } 
        return service;
    }
    
}
}
