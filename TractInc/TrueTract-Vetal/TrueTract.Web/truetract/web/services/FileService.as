package truetract.web.services
{
import mx.rpc.AbstractService;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.*;
import truetract.web.util.TokenResponder;

public class FileService
{
    
    //--------------------------------------------------------------------------
    //
    //  Singleton stuff
    //
    //--------------------------------------------------------------------------
    
    private static var _instance : FileService;
    public static function getInstance() : FileService
    {
        if ( _instance == null )
            _instance = new FileService(new SingletonEnforcer());
            
        return _instance;
    }

    public function FileService(singletonEnforcer:SingletonEnforcer) 
    {
    }

    //--------------------------------------------------------------------------
    //
    //  Class members
    //
    //--------------------------------------------------------------------------

    [Bindable] public var serviceIsBusy:Boolean = false;

    private var _service:RemoteObject;

    public function get service():RemoteObject
    {
        if (_service == null) {
           
            _service = new RemoteObject( "GenericDestination" );
            _service.source = "TractInc.TrueTract.File";
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

    public function addFile(file:File, uploadId:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.AddFile(file, uploadId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
            },
            "Unable to add File"
            )
        );

        return asyncToken;
    }

}
}
class SingletonEnforcer {}