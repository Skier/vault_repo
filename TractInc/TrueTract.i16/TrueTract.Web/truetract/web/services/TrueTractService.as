package truetract.web.services
{
import mx.rpc.AbstractService;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.DocumentFilter;
import truetract.domain.UserGroup;
import truetract.plotter.domain.Document;
import truetract.plotter.domain.Tract;

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

    public function saveUserGroup(group:UserGroup, newGroupName:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.SaveUserGroup(group.groupId, newGroupName);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                group.groupName = newGroupName;
            },
            function (event:FaultEvent):void
            {
                trace("saveUserGroup.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
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

    public function findDrawingsByTemplate(userId:int, refName:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.FindDrawingsByTemplate(userId, refName);
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

    public function loadUserItemsGroup(userId:int, docFiler:DocumentFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.LoadUserItemsGroup(userId, docFiler);
        asyncToken.addResponder(loadUserGroupResponder);

        return asyncToken;
    }

    public function loadUserRecentItemsGroup(userId:int, docFilter:DocumentFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.LoadUserRecentItemsGroup(userId, docFilter);
        asyncToken.addResponder(loadUserGroupResponder);

        return asyncToken;
    }

    public function loadUserGroup(groupId:int, userId:int, docFiler:DocumentFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.LoadUserGroup(groupId, userId, docFiler);
        asyncToken.addResponder(loadUserGroupResponder);

        return asyncToken;
    }

    public function getDocuments(docFilter:DocumentFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetDocuments(docFilter);
        asyncToken.addResponder ( new Responder(
            function (event:ResultEvent):void {
                
                var docs:Array = event.result as Array;
                
                resolveDuplicateConflict(docs, documentsHash, "DocID");
            },
            function (event:FaultEvent):void
            {
                trace("getDocuments.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
        
    }

    public function getDocumentBranchRevisions(docBranchId:String ):AsyncToken
    {
        return service.GetDocumentBranchRevisions(docBranchId);
    }

    public function saveTract(tract:Tract, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.SaveTract(tract, userId);
        asyncToken.addResponder ( new Responder(
            function (event:ResultEvent):void {
                var serverTract:Tract = Tract(event.result);

                if (tract.TractId == 0) {
                    tract.TractId = serverTract.TractId;

                    if (tract.ParentDocument) {
                        tract.ParentDocument.TractsList.addItem(tract);
                    }
                }

                if (tract.ParentDocument)
                    tract.ParentDocument.recalculateTractsCount();

                tract.IsDirty = false;
            },
            function (event:FaultEvent):void
            {
                trace("saveTract.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }

    public function saveDocument(document:Document, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.SaveDocument(document, userId);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                var serverDocument:Document = Document(event.result);
                
                if (document.DocID != 0) 
                {
                    var docHashRef:Document = documentsHash[document.DocID];
                    docHashRef.setFieldsValues(serverDocument);
                } 
                else 
                {
                    documentsHash[serverDocument.DocID] = serverDocument;
                }
            },
            function (event:FaultEvent):void
            {
                trace("saveDocument.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }

    public function activateDocumentRevision(document:Document, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.SaveDocument(document, userId);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                var serverDocument:Document = Document(event.result);
                
                if (document.DocID != 0) 
                {
                    var docHashRef:Document = documentsHash[document.DocID];
                    docHashRef.setFieldsValues(serverDocument);
                } 
                else 
                {
                    documentsHash[serverDocument.DocID] = serverDocument;
                }
            },
            function (event:FaultEvent):void
            {
                trace("saveDocument.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }
    
    private function resolveDuplicateConflict(
        items:Array, itemsHash:Object, itemsPKFieldName:String):void
    {
        if (!items) return;

        for (var i:int = 0; i < items.length; i++)
        {
            var pkValue:* = items[i][itemsPKFieldName];

            if (itemsHash[pkValue] != null) {
                items[i] = itemsHash[pkValue];
            } else {
                itemsHash[pkValue] = items[i];
            }
        }
    }
    
}
}