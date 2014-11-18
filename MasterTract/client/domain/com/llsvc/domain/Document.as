package com.llsvc.domain
{
import flash.events.EventDispatcher;

import mx.collections.ArrayCollection;
import mx.collections.ListCollectionView;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentEntity")]
public class Document extends EventDispatcher
{
    public var id:int;      
    public var documentType:DocumentType;
    public var documentStatus:DocumentStatus;
    public var user:User;
    public var note:String;
    public var attachments:ArrayCollection;
    
    public var actors:ArrayCollection;

    public var givers:ArrayCollection;
    public var receivers:ArrayCollection;

    public var references:ArrayCollection;
    public var refDocs:ArrayCollection;
    public var isRefDocsLoaded:Boolean = false;
    public var isRefDocLoading:Boolean = false;
    
    public var projects:ArrayCollection;

    public function Document()
    {
        documentType = new DocumentType();
        documentStatus = new DocumentStatus();
        actors = new ArrayCollection();
        records = new ArrayCollection();
        attachments = new ArrayCollection();
        
        givers = new ArrayCollection();
        receivers = new ArrayCollection();
        
        references = new ArrayCollection();
        refDocs = new ArrayCollection();
        
        projects = new ArrayCollection();
    }
    
    public function populate(value:Document):void 
    {
        this.id = value.id;
        this.note = value.note;
        this.user = value.user;
        this.documentType.populate(value.documentType);
        this.documentStatus.populate(value.documentStatus);
        
        this.actors.removeAll();
        for each (var actor:DocumentActor in value.actors) 
        {
            this.actors.addItem(actor);
        }
        checkOutActors();
         
        this.attachments.removeAll();
        this.records.removeAll();
        for each (var attachment:DocumentAttachment in value.attachments)
        {
        	attachment.document = this;
            this.attachments.addItem(attachment);
            
            if (attachment.record != null && attachment.type == DocumentAttachment.RECORDED_TYPE) 
            {
            	this.records.addItem(attachment.record);
            }
        }
        
        refreshRecords();
        
        this.references.removeAll();
        for each (var docRef:DocumentReference in value.references) 
        {
        	this.references.addItem(docRef);
        }
        
        this.projects.removeAll();
        for each (var dp:DocumentProject in value.projects) 
        {
        	this.projects.addItem(dp);
        }
        dispatchEvent(new Event("projectCollectionChange"));
    }
    
    public function createCopy():Document 
    {
        var result:Document = new Document();
        
        result.note = this.note;
        result.documentType = this.documentType.createCopy();
        result.documentStatus = this.documentStatus.createCopy();
        
        for each (var actor:DocumentActor in this.receivers) 
        {
            result.actors.addItem(actor.createCopy());
        }

        return result;
    }
    
    public function get giversStr():String 
    {
        var result:String = "";
        
        for each (var g:DocumentActor in givers) 
        {
            result += (result.length > 0 ? ", " : "");
            result += g.name;
        }
        
        return result;
    }
    
    public function get giversInfoStr():String 
    {
        var result:String = "";
        
        for each (var g:DocumentActor in givers) 
        {
            result += g.infoStr;
            result += "\n";
        }
        
        return result;
    }
    
    public function get recordsStr():String 
    {
        var result:String = "";
        
        for each (var rec:DocumentRecord in publicRecords) 
        {
            result += (rec.county.name + " ");
            result += (rec.docNo + ":" + rec.volume + "/" + rec.page);
            result += "; ";
        }
        
        return result;
    }
    
    public function get recordsAllStr():String 
    {
        var result:String = "";
        
        var rec:DocumentRecord;

        for each (rec in publicRecords) 
        {
            result += rec.getInfoStr();
            result += "\n";
        }
        
        for each (rec in privateRecords) 
        {
            result += rec.getInfoStr();
            result += "\n";
        }
        
        return result;
    }
    
//  different types of actors
    public function checkOutActors():void 
    {
        givers.removeAll();
        receivers.removeAll();
        for each (var actor:DocumentActor in actors) 
        {
            if (actor.isGiver) 
            {
                givers.addItem(actor);
            } else 
            {
                receivers.addItem(actor);
            }
        }
    }
    
    public function checkInActors():void 
    {
        actors.removeAll();
        
        var actor:DocumentActor;
        
        for each (actor in givers) 
        {
            actors.addItem(actor);
        }
        
        for each (actor in receivers) 
        {
            actors.addItem(actor);
        }
    }
    
//  ---------------

//  different types of records
    private var _records:ArrayCollection;
    public function get records():ArrayCollection {return _records}
    public function set records(value:ArrayCollection):void 
    {
        _records = value;
        
        privateRecords = new ListCollectionView(records);
        privateRecords.filterFunction = isPrivateRecord;
        
        publicRecords = new ListCollectionView(records);
        publicRecords.filterFunction = isPublicRecord;

        refreshRecords();
    }
    public var publicRecords:ListCollectionView;
    public var privateRecords:ListCollectionView;
    
    private function isPublicRecord(value:DocumentRecord):Boolean 
    {
        return value.isPublic;
    } 

    private function isPrivateRecord(value:DocumentRecord):Boolean 
    {
        return !value.isPublic;
    }
    
    public function refreshAttachments():void 
    {
    	for each (var record:DocumentRecord in records) 
    	{
	        for each (var attach:DocumentAttachment in attachments) 
	        {
/* to do: fix it	        	
        		if (attach.recordId == record.id) 
        		{
        			attach.record = record;
        			record.attachment = attach;
        		}
*/        		
        	}
        }
    } 
    
    public function refreshRecords():void 
    {
        privateRecords.refresh();
        publicRecords.refresh();
        
        refreshAttachments();
    }
//  ---------------

    public function setComplete(value:Boolean):void 
    {
        if (value) 
        {
            documentStatus.id = DocumentStatus.COMPLETE_ID;
            documentStatus.name = DocumentStatus.COMPLETE_NAME;
        } else 
        {
            documentStatus.id = DocumentStatus.DRAFT_ID;
            documentStatus.name = DocumentStatus.DRAFT_NAME;
        }
        
    }
    
}
}
