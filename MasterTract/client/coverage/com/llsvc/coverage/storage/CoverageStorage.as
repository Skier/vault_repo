package com.llsvc.coverage.storage
{
import com.llsvc.domain.CoverageTract;

import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.remoting.RemoteObject;

public class CoverageStorage 
    implements ICoverageStorage
{
    private static const SERVICE:String = "coverageService";
    
    private static var _instance:CoverageStorage;

    public static function get instance():CoverageStorage
    {
        if (_instance == null) {
            _instance = new CoverageStorage();
        }
        return _instance;
    }
    
    private var service:RemoteObject = null;
    
    public function CoverageStorage()
    {
        if (_instance != null) {
            throw new Error("Use instance getter instead of constructor. It's singleton !");
        }
    }
    
    public function findCoverageTracts(mask:CoverageTract, responder:Responder):void {
        var asyncToken:AsyncToken = getService().findCoverageTracts(mask);
        asyncToken.addResponder(responder);
    }
    
    public function saveCoverageTract(coverageTract:CoverageTract, responder:Responder):void {
        var asyncToken:AsyncToken = getService().saveCoverageTract(coverageTract);
        asyncToken.addResponder(responder);
    }
    
    public function removeCoverageTract(coverageTractId:int, responder:Responder):void {
        var asyncToken:AsyncToken = getService().removeCoverageTract(coverageTractId);
        asyncToken.addResponder(responder);
    }
    
    private function getService():RemoteObject {
        if ( null == service ) {
            service = new RemoteObject();
            service.destination = CoverageStorage.SERVICE;
            service.source = CoverageStorage.SERVICE;
        } 
        return service;
    }
    
}
}
