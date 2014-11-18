package tractIncClientApp.services
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
import mx.core.Application;
import flash.events.Event;
import tractIncClientApp.document.DocumentEvent;
import tractIncClientApp.ClientController;

public class DocumentService
{
    
    //--------------------------------------------------------------------------
    //
    //  Singleton stuff
    //
    //--------------------------------------------------------------------------
    
    private static var _instance : DocumentService;
    public static function getInstance() : DocumentService
    {
        if ( _instance == null )
            _instance = new DocumentService(new SingletonEnforcer());
            
        return _instance;
    }

    public function DocumentService(singletonEnforcer:SingletonEnforcer) 
    {
        clean();

        Application.application.addEventListener("logout", 
            function(event:Event):void { clean(); });
    }

    //--------------------------------------------------------------------------
    //
    //  Class members
    //
    //--------------------------------------------------------------------------

    [Bindable] public var serviceIsBusy:Boolean = false;

    private var _service:RemoteObject;

    private var documentsHash:Object;
    private var drawingsHash:Object;

    private var getDocumentsResponder:Responder = new Responder(
        function (event:ResultEvent):void {
            resolveDuplicateConflict(event.result as Array, documentsHash, "DocID");
        },
        function (event:FaultEvent):void {
            trace("LoadDocuments.onFault: " + event.fault.faultString);
        }
    );

    private var getDrawingsResponder:Responder = new Responder(
        function (event:ResultEvent):void {
            resolveDuplicateConflict(event.result as Array, drawingsHash, "TractId");
        },
        function (event:FaultEvent):void {
            trace("LoadDrawings.onFault: " + event.fault.faultString);
        }
    );

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
        drawingsHash = new Object();
        documentsHash = new Object();
    }

    public function getStoredDocument(documentId:int):Document
    {
        return documentsHash[documentId];
    }

    public function getDocument(docId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetDocument(docId);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void {
                var doc:Document = event.result as Document;
                
                var docHashRef:Document = documentsHash[docId];
                
                if (docHashRef != null) 
                {
                    docHashRef.setFieldsValues(doc);
                } else 
                {
                    documentsHash[docId] = doc;
                }
            },
            function (event:FaultEvent):void {
                trace("LoadDocuments.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }

    public function getDocuments(filter:IItemsFilter, canBeInactive:Boolean = false):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetDocuments(filter, canBeInactive);
        asyncToken.addResponder(getDocumentsResponder);

        return asyncToken;
    }

    public function getUserDocuments(userId:int, filter:IItemsFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetUserDocuments(userId, filter);
        asyncToken.addResponder(getDocumentsResponder);

        return asyncToken;
    }

    public function containsDocument(documentId:int):Boolean
    {
        return documentsHash[documentId] != null;
    }
    
    public function getUserDrawings(userId:int, filter:IItemsFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetUserDrawings(userId, filter);
        asyncToken.addResponder(getDrawingsResponder);

        return asyncToken;
    }

    public function getUserRecentDocuments(userId:int, filter:IItemsFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetUserRecentDocuments(userId, filter);
        asyncToken.addResponder(getDocumentsResponder);

        return asyncToken;
    }

    public function getGroupDocuments(groupId:int, userId:int, filter:IItemsFilter):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetGroupDocuments(groupId, userId, filter);
        asyncToken.addResponder(getDocumentsResponder);

        return asyncToken;
    }

    public function getDocumentBranchRevisions(docBranchId:String):AsyncToken
    {
        return service.GetDocumentBranchRevisions(docBranchId);
    }

    public function getDocumentTractList(documentId:int):AsyncToken
    {
        return service.GetDocumentTracts(documentId);
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
                    } else {
                        drawingsHash[tract.TractId] = tract;                    
                    }
                }

                if (tract.ParentDocument) {
                    tract.ParentDocument.recalculateTractsCount();
//                    saveDocument(tract.ParentDocument, userId);
                }
                tract.IsDirty = false;
            },
            function (event:FaultEvent):void
            {
                trace("saveTract.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }

    public function deleteTract(tract:Tract, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.DeleteTract(tract.TractId, userId);
        asyncToken.addResponder ( new Responder(
            function (event:ResultEvent):void 
            {
                delete drawingsHash[tract.TractId];

                if (tract.ParentDocument) {
                    var itemIndex:int = tract.ParentDocument.TractsList.getItemIndex(tract);
                    tract.ParentDocument.TractsList.removeItemAt(itemIndex);
                    tract.ParentDocument.recalculateTractsCount();
//                    saveDocument(tract.ParentDocument, userId);
                }
            },
            function (event:FaultEvent):void
            {
                trace("deleteTract.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }
    
    public function loadTract(tractId:int):AsyncToken
    {
        return service.LoadTract(tractId);
    }

    public function saveDocument(document:Document, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.SaveDocument(document, userId);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                var serverDocument:Document = Document(event.result);
                
                for each (var dr:DocumentReference in serverDocument.References) 
                {
                    var d:Document = documentsHash[dr.ReferenceId] as Document;
                    
                    if (d != null) 
                    {
                        dr.ReferencedDoc = d;
                    }
                }
                
                if (document.DocID != 0) 
                {
                    var docHashRef:Document = documentsHash[document.DocID];
                    documentsHash[document.DocID] = null;
                    documentsHash[serverDocument.DocID] = docHashRef;
                    docHashRef.setFieldsValues(serverDocument, true);
                    Application.application.dispatchEvent(new DocumentEvent(DocumentEvent.SAVE_DOCUMENT, docHashRef));
                } 
                else 
                {
                    documentsHash[serverDocument.DocID] = serverDocument;
                }
                
                for each (var tract:Tract in document.TractsList) 
                {
                    tract.IsDirty = false;
                }
                    
// kostil !!!!
                var doc:Document = Document(documentsHash[serverDocument.DocID]);
                if (doc.referencedProject) 
                {
                    ProjectService.getInstance().actualizeDocument(doc.referencedProject, doc);
                }

                if (doc.parentReference) 
                {
                    actualizeDocumentReference(doc.parentReference);
                }
// -----------

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
    
    public function loadDocumentReferences(document:Document):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetDocumentReferences(document.DocID);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                var serverDocument:Document = Document(event.result);
                
                if (document.DocID != 0) 
                {
                    document.setFieldsValues(serverDocument);
                    documentsHash[document.DocID] = document;
                } 
                else 
                {
                    documentsHash[serverDocument.DocID] = serverDocument;
                }
                
                for each (var docRef:DocumentReference in serverDocument.References) 
                {
                    docRef.ParentDocumentRef = document;
                    
                    if (docRef.ReferencedDoc != null) 
                    {
                        var doc:Document = documentsHash[docRef.ReferenceId];
                        
                        if (doc != null) {
                            doc.setFieldsValues(docRef.ReferencedDoc);
                            docRef.ReferencedDoc = doc;
                        } else {
                            documentsHash[docRef.ReferenceId] = docRef.ReferencedDoc;
                        }
                        
                        docRef.updateLocalFields();
                    }
                    
                    
                }
            },
            function (event:FaultEvent):void
            {
                trace("getDocumentReferences.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }
    
    public function addReference(reference:DocumentReference):void
    {
        var document:Document = documentsHash[reference.DocumentId];
        
        var docToken:AsyncToken = saveDocument(document, ClientController.getInstance().user.UserId);
        docToken.addResponder( new Responder(
            function (e:ResultEvent):void 
            {
                reference.DocumentId = Document(e.result).DocID;
                
                var asyncToken:AsyncToken = service.AddReference(reference);
                asyncToken.addResponder(new TokenResponder(
                    function (event:ResultEvent):void
                    {
                        var reference:DocumentReference = DocumentReference(event.result);
                        
                        var document:Document = documentsHash[reference.DocumentId];
                        document.addReference(reference);
                        
                        if (reference.ReferencedDoc != null) 
                        {
                            var docRef:Document = documentsHash[reference.ReferenceId];
                            
                            if (docRef != null) 
                            {
                                docRef.setFieldsValues(reference.ReferencedDoc);
                                reference.ReferencedDoc = docRef;
                            } else 
                            {
                                documentsHash[reference.ReferenceId] = reference.ReferencedDoc;
                            }
                        }
                    },
                    "Unable to add Reference"
                    )
                );
            },
            function (event:FaultEvent):void {}
            ));

    }
    
    public function saveReference(ref:DocumentReference):AsyncToken
    {
        var asyncToken:AsyncToken = service.SaveReference(ref, ClientController.getInstance().user.UserId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var reference:DocumentReference = DocumentReference(event.result);
                
                if (reference.ReferencedDoc != null) 
                {
                    var docRef:Document = documentsHash[reference.ReferenceId];
                    
                    if (docRef != null) 
                    {
                        docRef.setFieldsValues(reference.ReferencedDoc);
                        reference.ReferencedDoc = docRef;
                    } else 
                    {
                        documentsHash[reference.ReferenceId] = reference.ReferencedDoc;
                    }
                }
            },
            "Unable to add Reference"
            )
        );
        
        return asyncToken;
    }
    
    public function deleteReference(reference:DocumentReference):AsyncToken
    {
        var asyncToken:AsyncToken = service.DeleteReference(reference);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void {},
            "Unable to delete Reference"
            )
        );
        
        return asyncToken;
    }
    
    public function actualizeDocumentReference(reference:DocumentReference):void 
    {
        var document:Document = getStoredDocument(reference.DocumentId);
        
        saveDocument(document, ClientController.getInstance().user.UserId).addResponder( new Responder(
            function (e:ResultEvent):void 
            {
                var newDoc:Document = e.result as Document;
                
                for each (var ref:DocumentReference in newDoc.ReferencesList) 
                {
                    if (ref.ReferenceId == reference.ReferenceId) 
                    {
                        reference = ref;
                        break;
                    }
                }
    
                service.ActualizeDocumentReference(reference.DocumentReferenceId).addResponder(new TokenResponder(
                    function (event:ResultEvent):void
                    {
                        var docRef:DocumentReference = event.result as DocumentReference;
                        
                        reference.DocumentReferenceId = docRef.DocumentReferenceId;
                        reference.DocumentId = docRef.DocumentId;
                        reference.ReferenceId = docRef.ReferenceId;
                        reference.ReferencedDoc = docRef.ReferencedDoc;
                        
                        reference.updateLocalFields();
                        
                        var docHashRef:Document = documentsHash[reference.ReferenceId];
                        
                        if (docHashRef != null) 
                        {
                            docHashRef.setFieldsValues(reference.ReferencedDoc);
                            reference.ReferencedDoc = docHashRef;
                        } else 
                        {
                            documentsHash[reference.ReferenceId] = reference.ReferencedDoc;
                        }
                    },
                    "Unable to actualize Reference"
                    ));
            },
            function (event:FaultEvent):void {}
            ));
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

    public function addAttachment(attachment:DocumentAttachment, uploadId:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.AddAttachment(attachment, uploadId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                attachment.setMemento(DocumentAttachment(event.result).getMemento());

                var document:Document = documentsHash[attachment.DocumentId];
                document.addAttachment(attachment);
            },
            "Unable to add Attachment"
            )
        );

        return asyncToken;
    }

    public function updateAttachment(attachment:DocumentAttachment, uploadId:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.UpdateAttachment(attachment, uploadId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                attachment.setMemento(DocumentAttachment(event.result).getMemento());
            },
            "Unable to update Attachment"
            )
        );

        return asyncToken;
    }

    public function deleteAttachment(attachment:DocumentAttachment):AsyncToken
    {
        var asyncToken:AsyncToken = service.DeleteAttachment(attachment);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var doc:Document = documentsHash[attachment.DocumentId];
                if (doc) {
                    doc.deleteAttachment(attachment);               
                }
            },
            "Unable to delete Attachment"
            )
        );

        return asyncToken;
    }

}
}
class SingletonEnforcer {}