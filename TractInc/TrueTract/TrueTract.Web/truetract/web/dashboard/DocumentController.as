package truetract.web.dashboard
{
import mx.controls.Alert;
import mx.core.Application;
import mx.events.DynamicEvent;
import mx.events.PropertyChangeEvent;
import mx.events.PropertyChangeEventKind;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.ResultEvent;
import mx.utils.ObjectUtil;

import truetract.domain.DictionaryRegistry;
import truetract.domain.Document;
import truetract.domain.DocumentAttachment;
import truetract.web.AppModel;
import truetract.web.dashboard.file.FileAttachmentEditor;
import truetract.web.services.DocumentService;
import truetract.web.util.TokenResponder;
import flash.events.Event;
import truetract.domain.mementos.DocumentAttachmentMemento;
import truetract.domain.DocumentReference;
import truetract.web.dashboard.reference.DocumentReferenceEditor;
import truetract.domain.mementos.DocumentReferenceMemento;

public class DocumentController
{
    public var document:Document;

    private var documentService:DocumentService = DocumentService.getInstance();
    
    private var app:Application = Application(Application.application);

    public function addAttachment():void
    {
        var attachment:DocumentAttachment = new DocumentAttachment();

        var popup:FileAttachmentEditor = FileAttachmentEditor.open(app, true);
        popup.attachment = attachment;
        popup.attachmentTypes = DictionaryRegistry.getInstance().documentAttachmentTypes;
        popup.addEventListener("commit", function ():void 
        {
        	if (document.isAttachmentExists(attachment.FileRef.FileName)) 
        	{
        		Alert.show("This file is already attached");
        		return;
        	}
        	
            if (attachment.IsPdfCopy())
            {
            	if (document.PdfCopy) 
            	{
	                Alert.show("The PDF Copy of the Document is already specified");
	                return;
            	} else 
            	{
            		document.PdfCopy = attachment;
            	}
            }
            
            var docToken:AsyncToken = documentService.saveDocument(document, AppModel.getInstance().user.UserId);
            docToken.addResponder( new Responder(
            	function (e:ResultEvent):void 
            	{
			        attachment.DocumentId = document.DocID;
		    
		            var token:AsyncToken = documentService.addAttachment(attachment, popup.uploadID);
		            token.addResponder(new Responder(
		                function (event:ResultEvent):void 
		                {
		                    document.dispatchEvent(new PropertyChangeEvent(
		                        PropertyChangeEvent.PROPERTY_CHANGE, false, false, 
		                        PropertyChangeEventKind.UPDATE, "PdfCopy"));
		
		                    popup.close();
		                },
		                function (event:FaultEvent):void {}
		                ));
                },
                function (event:FaultEvent):void {}
                ));
        });
    }
    
    public function editAttachment(attachment:DocumentAttachment):void
    {
        var memento:DocumentAttachmentMemento = 
            DocumentAttachmentMemento(attachment.getMemento());

        var popup:FileAttachmentEditor = FileAttachmentEditor.open(app, true);
        popup.attachment = attachment;
        popup.attachmentTypes = DictionaryRegistry.getInstance().documentAttachmentTypes;
        popup.addEventListener("commit", 
            function (event:Event):void
            {
	            var docToken:AsyncToken = documentService.saveDocument(document, AppModel.getInstance().user.UserId);
	            docToken.addResponder( new Responder(
	            	function (e:ResultEvent):void 
	            	{
		                var token:AsyncToken = DocumentService.getInstance().updateAttachment(attachment, popup.uploadID);
		                token.addResponder(new Responder(
		                    function(event:ResultEvent):void
		                    {
		                        popup.close();
		                    },
		                    function(event:FaultEvent):void {}
		                ));
	                },
	                function (event:FaultEvent):void {}
	            ));
            }
        )
        popup.addEventListener("cancel", 
            function():void
            {
                attachment.setMemento(memento);
            }
        );
    }
    
    public function deleteAttachment(attachment:DocumentAttachment):void
    {
        var docToken:AsyncToken = documentService.saveDocument(document, AppModel.getInstance().user.UserId);
        docToken.addResponder( new Responder(
        	function (e:ResultEvent):void 
        	{
        		var newDoc:Document = e.result as Document;
				
        		for each (var attach:DocumentAttachment in newDoc.AttachmentsList) 
        		{
        			if (attach.FileRef.Description == attachment.FileRef.Description
        				&& attach.FileRef.FileName == attachment.FileRef.FileName) 
        			{
        				attachment = attach;
        				attachment.DocumentId = newDoc.DocID;
        				break;
        			}
        		}
		    
		        var token:AsyncToken = DocumentService.getInstance().deleteAttachment(attachment);
		
		        token.addResponder(new Responder(
		            function(event:ResultEvent):void 
		            {
		                document.dispatchEvent(new PropertyChangeEvent(
		                    PropertyChangeEvent.PROPERTY_CHANGE, false, false, 
		                    PropertyChangeEventKind.UPDATE, "PdfCopy"));
		            }, 
		            function(event:FaultEvent):void {
		            }
		        ));
            },
            function (e:FaultEvent):void {}
        ));
    }
    
    public function addReference():void 
    {
    	var docRef:DocumentReference = new DocumentReference();
    	
    	var popup:DocumentReferenceEditor = DocumentReferenceEditor.open(app, true);
    	popup.documentReference = docRef;
    	popup.addEventListener("submit", 
    		function(event:Event):void 
    		{
	    		if (document.State == docRef.State 
	    			&& document.County == docRef.County
	    			&& document.DocTypeId == docRef.DocTypeId
	    			&& (document.DocumentNo == docRef.DocumentNo 
	    				|| (document.Volume == docRef.Volume && document.Page == docRef.Page))) 
	    		{
    				Alert.show("Document can't have reference to itself!", "Reference error");
    				return;
	    		}

	   			if (document.isReferenceExists(docRef)) 
    			{
    				Alert.show("Reference to this document already exists!", "Reference error");
    				return;
    			}
    				
	            var docToken:AsyncToken = documentService.saveDocument(document, AppModel.getInstance().user.UserId);
	            docToken.addResponder( new Responder(
	            	function (e:ResultEvent):void 
	            	{
				        docRef.DocumentId = Document(e.result).DocID;
			    
		    			var asyncToken:AsyncToken = documentService.saveReference(docRef);
		    			asyncToken.addResponder( 
		    				new Responder(
		    					function (event:ResultEvent):void 
		    					{
		    						var savedReference:DocumentReference = DocumentReference(event.result);
		    						
		    						document.ReferencesList.addItem(savedReference);
		    						savedReference.ParentDocumentRef = document;
		    						popup.close();
		    					},
		    					function (event:FaultEvent):void 
		    					{
		    					}
		    				));
	                },
	                function (event:FaultEvent):void 
	                {
   						Alert.show("Can't save document:" + event.fault.faultString);
	                }
	                ));
    		});
    }

    public function editReference(docRef:DocumentReference):void 
    {
    	var memento:DocumentReferenceMemento = docRef.getMemento() as DocumentReferenceMemento;
    	
    	var popup:DocumentReferenceEditor = DocumentReferenceEditor.open(app, true);
    	popup.documentReference = docRef;
    	popup.isReadOnly = !document.IsActive;
    	
    	popup.addEventListener("submit", 
    		function(event:Event):void 
    		{
	    		if (document.State == docRef.State 
	    			&& document.County == docRef.County
	    			&& document.DocTypeId == docRef.DocTypeId
	    			&& (document.DocumentNo == docRef.DocumentNo 
	    				|| (document.Volume == docRef.Volume && document.Page == docRef.Page))) 
	    		{
    				Alert.show("Document can't have reference to itself!", "Reference error");
    				return;
	    		}

    			if (document.isReferenceExists(docRef)) 
    			{
    				Alert.show("Reference to this document already exists!", "Reference exists");
    				return;
    			}
    				
	            var docToken:AsyncToken = documentService.saveDocument(document, AppModel.getInstance().user.UserId);
	            docToken.addResponder( new Responder(
	            	function (e:ResultEvent):void 
	            	{
	            		var newRef:DocumentReference = null;
	            		var oldRef:DocumentReference = new DocumentReference();
	            		oldRef.setMemento(memento);
	            		
				    	for each (var ref:DocumentReference in document.ReferencesList)
				    	{
				    		if (ref.State == oldRef.State 
				    			&& ref.County == oldRef.County
				    			&& ref.DocTypeId == oldRef.DocTypeId
				    			&& (ref.DocumentNo == oldRef.DocumentNo 
				    				|| (ref.Volume == oldRef.Volume && ref.Page == oldRef.Page))) 
				    		{
				    			newRef = ref;
				    			break;
				    		}
				    	}
				    	
				    	if (newRef == null) 
				    		throw new Error("Problem with finding new reference in the document");
				    	
				    	docRef.DocumentReferenceId = newRef.DocumentReferenceId;
				    	docRef.DocumentId = newRef.DocumentId;
			    		
		    			var asyncToken:AsyncToken = documentService.saveReference(docRef);
		    			asyncToken.addResponder( 
		    				new Responder(
		    					function (event:ResultEvent):void 
		    					{
		    						newRef.setMemento(DocumentReference(event.result).getMemento());
		    						
		    						newRef.ReferencedDoc = DocumentReference(event.result).ReferencedDoc;
		    						newRef.ParentDocumentRef = document;
		    						popup.close();
		    					},
		    					function (event:FaultEvent):void 
		    					{
		    					}
		    				));
	                },
	                function (event:FaultEvent):void 
	                {
   						Alert.show("Can't save document:" + event.fault.faultString);
	                }
	                ));
    		});

    	popup.addEventListener("cancel", 
    		function(event:Event):void 
    		{
    			docRef.setMemento(memento);
    		});
    }

    public function deleteReference(docRef:DocumentReference):void 
    {
	    var docToken:AsyncToken = documentService.saveDocument(document, AppModel.getInstance().user.UserId);
	    docToken.addResponder( new Responder(
	    	function (e:ResultEvent):void 
	    	{
		        docRef.DocumentId = Document(e.result).DocID;
		    	
		    	for each (var ref:DocumentReference in document.ReferencesList)
		    	{
		    		if (ref.State == docRef.State 
		    			&& ref.County == docRef.County
		    			&& ref.DocTypeId == docRef.DocTypeId
		    			&& (ref.DocumentNo == docRef.DocumentNo 
		    				|| (ref.Volume == docRef.Volume && ref.Page == docRef.Page))) 
		    		{
		    			docRef = ref;
		    			break;
		    		}
		    	}
	    
				var asyncToken:AsyncToken = documentService.deleteReference(docRef);
				asyncToken.addResponder( 
					new Responder(
						function (event:ResultEvent):void 
						{
							var idx:int = document.ReferencesList.getItemIndex(docRef);
							if (idx > -1) 
							{
								document.ReferencesList.removeItemAt(idx);
							}
						},
						function (event:FaultEvent):void 
						{
						}
					));
	        },
	        function (event:FaultEvent):void 
	        {
				Alert.show("Can't save document:" + event.fault.faultString);
	        }
	        ));
    }
}

}