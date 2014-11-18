package truetract.web.services
{
import mx.rpc.AbstractService;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.plotter.domain.Document;
import truetract.web.dashboard.UserGroup;

public class TrueTractService
{
    
    //--------------------------------------------------------------------------
    //
    //  Singleton stuff
    //
    //--------------------------------------------------------------------------
    
    private static var _instance : TrueTractService;
    public static function getInstance() : TrueTractService
    {
        if ( _instance == null )
            _instance = new TrueTractService( arguments.callee );
            
        return _instance;
    }

    public function TrueTractService( caller : Function = null ) 
    {
        if(caller != TrueTractService.getInstance)
        {
            throw new Error ("TrueTractService is a singleton class, use getInstance() instead");
        }
        
        if (TrueTractService._instance != null)
        {
            throw new Error( "Only one TrueTractService instance should be instantiated" ); 
        }
    }

    //--------------------------------------------------------------------------
    //
    //  Class members
    //
    //--------------------------------------------------------------------------

    [Bindable] public var serviceIsBusy:Boolean = false;
    
    private var _service:RemoteObject;

    private var documentsHash:Object = new Object();
    private var drawingsHash:Object = new Object();

    private var loadUserGroupResponder:Responder = new Responder(
        function (event:ResultEvent):void
        {
            var group:UserGroup = UserGroup(event.result);
            
            resolveDuplicateConflict(group.groupDocuments, documentsHash, "DocID");
            resolveDuplicateConflict(group.groupDrawings, drawingsHash, "TractId");
        },
        function (event:FaultEvent):void
        {
            trace("LoadUserGroup.onFault: " + event.fault.faultString);
        });
            
    public function get service():RemoteObject
    {
        if (_service == null) {
           
            _service = new RemoteObject( "GenericDestination" );
            _service.source = "TractInc.TrueTract.TrueTractService";
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

    public function findDocumentsByTemplate(template:Document):AsyncToken
    {
        var asyncToken:AsyncToken = service.FindDocumentsByTemplate(template);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                resolveDuplicateConflict(event.result as Array, documentsHash, "DocID");
            },
            function (event:FaultEvent):void
            {
                trace("FindDocumentsByTemplate.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }

    public function findDrawingsByTemplate(refName:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.FindDrawingsByTemplate(refName);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                resolveDuplicateConflict(event.result as Array, drawingsHash, "TractId");
            },
            function (event:FaultEvent):void
            {
                trace("FindDrawingsByTemplate.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }

    public function loadUserItemsGroup(userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.LoadUserItemsGroup(userId);
        asyncToken.addResponder(loadUserGroupResponder);

        return asyncToken;
    }

    public function loadUserRecentItemsGroup(userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.LoadUserRecentItemsGroup(userId);
        asyncToken.addResponder(loadUserGroupResponder);

        return asyncToken;
    }

    public function loadUserGroup(groupId:int, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.LoadUserGroup(groupId, userId);
        asyncToken.addResponder(loadUserGroupResponder);

        return asyncToken;
    }

    private function resolveDuplicateConflict(
        items:Array, itemsHash:Object, itemsPKFieldName:String):void
    {
        if (!items) return;

        for (var i:int = 0; i < items.length; i++)
        {
            var pkValue:* = items[i][itemsPKFieldName];

            if (itemsHash[itemsPKFieldName] != null) {
                items[i] = itemsHash[pkValue];
            } else {
                itemsHash[pkValue] = items[i];
            }
        }
    }
    
}
}