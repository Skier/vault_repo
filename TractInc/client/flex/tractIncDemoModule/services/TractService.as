package services
{
import mx.rpc.AbstractService;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.*;
import mx.core.Application;
import flash.events.Event;

public class TractService
{
    
    private static var _instance : TractService;
    public static function getInstance() : TractService
    {
        if ( _instance == null )
            _instance = new TractService(new SingletonEnforcer());
            
        return _instance;
    }

    public function TractService(singletonEnforcer:SingletonEnforcer) 
    {
        clean();
    }

    [Bindable] public var serviceIsBusy:Boolean = false;

    private var _service:RemoteObject;

    private function get service():RemoteObject
    {
        if (_service == null) {
           
            _service = new RemoteObject( "GenericDestination" );
            _service.source = "TractInc.TrueTract.Document";
            _service.showBusyCursor = true;
            
            _service.addEventListener(InvokeEvent.INVOKE, 
                function(event:InvokeEvent):void { serviceIsBusy = true });
            
            _service.addEventListener(ResultEvent.RESULT,
                function(event:ResultEvent):void { serviceIsBusy = false });
            
            _service.addEventListener(FaultEvent.FAULT,
                function(event:FaultEvent):void { serviceIsBusy = false });
        }

        return _service;
    }

    public function clean():void
    {
    }

    public function loadTract(tractId:int):AsyncToken
    {
        return service.LoadTract(tractId);
    }

}
}
class SingletonEnforcer {}
