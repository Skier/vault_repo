package truetract.domain
{
import flash.events.EventDispatcher;
import flash.utils.Timer;
import flash.utils.getTimer;

import mx.collections.ArrayCollection;
import mx.events.FlexEvent;
import mx.events.PropertyChangeEvent;
import mx.formatters.DateFormatter;
import mx.states.State;
import mx.events.PropertyChangeEventKind;

[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentInfo")]
public class Document extends EventDispatcher
{
    public var DocID:int;
    public var DocBranchUid:String;
    public var DocTypeId:int;
    public var Volume:String;
    public var Page:String;
    public var DocumentNo:String;
    public var County:int;
    public var State:int;
    public var DateFiledYear:int;
    public var DateFiledMonth:int;
    public var DateFiledDay:int;
    public var DateSignedYear:int;
    public var DateSignedMonth:int;
    public var DateSignedDay:int;
    public var ResearchNote:String;
    public var ImageLink:String;
    public var CreatedBy:int;
    public var Created:Date;
    public var IsActive:Boolean;
    public var IsPublic:Boolean;
    public var IsLoaded:Boolean;
    public var IsReferencesLoaded:Boolean = false;
    public var PreviousVersion:int;

    public var TractsCount:int = 0;
    public var TractsAcres:Number = 0;

    public var CreatedByName:String;

// kostyl !!!    
    public var referencedProject:Project;
    public var parentReference:DocumentReference;
//-----------

    /**
    * Tracts List
    */
    private var _tractsList:ArrayCollection = new ArrayCollection();
    public function get TractsList():ArrayCollection { return _tractsList; }

    private var _tracts:Array;
    public function get Tracts():Array { return _tracts; }
    public function set Tracts(value:Array):void 
    {
        TractsList.source = _tracts = value;
        
        if (value && value.length > 0)
        {
            for each (var tract:Tract in value)
            {
                tract.ParentDocument = this;
            }
        }
    }

    /**
    * Attachments List
    */
    private var _attachmentsList:ArrayCollection = new ArrayCollection();
    public function get AttachmentsList():ArrayCollection { return _attachmentsList; }

    private var _attachments:Array;
    public function get Attachments():Array { return _attachments; }
    public function set Attachments(value:Array):void 
    {
        AttachmentsList.source = _attachments = value;
    }

    /**
    * Buyer
    */
    private var _buyer:Participant = new Participant;
    public function get Buyer():Participant { return _buyer; }
    public function set Buyer(value:Participant):void
    {
        _buyer = value;
        
        if (value != null)
        {
            _buyer.IsSeler = false;
            var docInstance:Document = this;
            
            value.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE,
                function(event:PropertyChangeEvent):void
                {
                    docInstance.dispatchEvent(event);
                })
        }
    }

    /**
    * Seller
    */
    private var _seller:Participant = new Participant;
    public function get Seller():Participant { return _seller; }
    public function set Seller(value:Participant):void
    {
        _seller = value;
        
        if (value != null)
        {
            _seller.IsSeler = true;

            var docIntance:Document = this;

            value.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE,
                function(event:PropertyChangeEvent):void
                {
                    docIntance.dispatchEvent(event);
                })
        }
    }

    /**
    * References List
    */
    private var _referencesList:ArrayCollection = new ArrayCollection();
    public function get ReferencesList():ArrayCollection { return _referencesList; }

    private var _references:Array;
    public function get References():Array { return _references; }
    public function set References(value:Array):void 
    {
        ReferencesList.source = _references = value;
    }

    /**
    * Lease
    */
    private var _lease:DocumentLease = null;
    public function get Lease():DocumentLease { return _lease; }
    public function set Lease(value:DocumentLease):void
    {
        _lease = value;
    }

    public function Document()
    {
        DateFiled = new Date();
        DateSigned = new Date();
        
        Buyer = new Participant;
        Seller = new Participant;
    }

    public function set PdfCopy(value:DocumentAttachment):void {}
    public function get PdfCopy():DocumentAttachment
    {
        var result:DocumentAttachment;

        for each (var file:DocumentAttachment in AttachmentsList)
        {
            if (file.IsPdfCopy())
            {
                result = file;
                break;
            }
        }

        return result;
    }

    public function get SellerName():String
    {
        return Seller ? Seller.AsNamed : null;
    }

    public function get BuyerName():String
    {
        return Buyer ? Buyer.AsNamed : null;
    }

    public function get DocumentType():XMLList
    {
        return DictionaryRegistry.getInstance().getDocumentType(DocTypeId);
    }
    
    public function get CountyName():String
    {   
        var result:String = DictionaryRegistry.getInstance().getCountyName(State, County);
        return result ? result : "";
    }
    
    public function get StateName():String
    {
        return DictionaryRegistry.getInstance().getState(State).@Name;
    }
    
    public function get DocumentTypeName():String
    {
        return DocumentType.@Name;
    }
    
    public function get SellerRoleName():String
    {
        return DocumentType.@GiverRoleName;
    }
    
    public function get BuyerRoleName():String
    {
        return DocumentType.@ReceiverRoleName;
    }
    
    public function get DateSigned():Date
    {
        return new Date(DateSignedYear, DateSignedMonth - 1, DateSignedDay);
    }

    public function get Description():String
    {
        return toString();
    }
    
    public function set DateSigned(value:Date):void
    {
        DateSignedYear = value ? value.getFullYear() : null;
        DateSignedMonth = value ? value.getMonth() + 1 : null;
        DateSignedDay = value ? value.getDate() : null;
    }

    public function get DateFiled():Date
    {
        return new Date(DateFiledYear, DateFiledMonth -1, DateFiledDay);
    }

    public function set DateFiled(value:Date):void
    {
        DateFiledYear = value ? value.getFullYear() : null;
        DateFiledMonth = value ? value.getMonth()+ 1 : null;
        DateFiledDay = value ? value.getDate() : null;
    }

    public function get CreatedString():String
    {
        var df:DateFormatter = new DateFormatter();
        df.formatString = "DD MMM YYYY J:NN";

        return df.format(Created);
    }

    public function get DocumentPlacement():String
    {
        return (DocumentNo && DocumentNo.length > 0) 
            ? 'Doc.No:' + DocumentNo
            : 'Vol:' + Volume + ', Pg:' + Page;
    }

    public function get Instrument():String 
    {
        return (DocumentTypeName + " " + DocumentPlacement);
    }

    public function addAttachment(attachment:DocumentAttachment):DocumentAttachment
    {
        attachment.DocumentId = DocID;
        attachment.DocumentRef = this;

        AttachmentsList.addItem(attachment);

        return attachment;
    }

    public function deleteAttachment(attachment:DocumentAttachment):void
    {
        var itemIndex:int = AttachmentsList.getItemIndex(attachment);
        if (itemIndex > -1)
            AttachmentsList.removeItemAt(itemIndex);
    }
    
    public function existsReference(doc:Document):Boolean 
    {
        for each (var ref:DocumentReference in ReferencesList) 
        {
            if (ref.ReferencedDoc != null && ref.ReferencedDoc.DocBranchUid == doc.DocBranchUid) 
            {
                return true;
            }
        }
        
        return false;
    }
    
    public function addReference(reference:DocumentReference):DocumentReference
    {
        reference.DocumentId = DocID;
        reference.ParentDocumentRef = this;
        
        ReferencesList.addItem(reference);

        return reference;
    }

    public function deleteReference(reference:DocumentReference):void
    {
        var itemIndex:int = ReferencesList.getItemIndex(reference);
        if (itemIndex > -1)
            ReferencesList.removeItemAt(itemIndex);
    }

    public function setFieldsValues(sourceDoc:Document, withReferences:Boolean = false):void
    {
        if (!sourceDoc) return;

        DocID = sourceDoc.DocID;
        DocTypeId = sourceDoc.DocTypeId;
        Volume = sourceDoc.Volume;
        Page = sourceDoc.Page;
        DocumentNo = sourceDoc.DocumentNo;
        County = sourceDoc.County;
        State = sourceDoc.State;
        DateFiledYear = sourceDoc.DateFiledYear;
        DateFiledMonth = sourceDoc.DateFiledMonth;
        DateFiledDay = sourceDoc.DateFiledDay;
        DateSignedYear = sourceDoc.DateSignedYear;
        DateSignedMonth = sourceDoc.DateSignedMonth;
        DateSignedDay = sourceDoc.DateSignedDay;
        PreviousVersion = sourceDoc.PreviousVersion;

        Buyer = sourceDoc.Buyer;
        Seller = sourceDoc.Seller;

        Tracts = sourceDoc.Tracts;

        Attachments = sourceDoc.Attachments;
        dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, false, false, 
            PropertyChangeEventKind.UPDATE, "PdfCopy"));
/* 
        if (sourceDoc.References != null 
            && sourceDoc.References.length > 0 
            && DocumentReference(sourceDoc.References[0]).ReferencedDoc != null) 
        {
            References = sourceDoc.References;
        }
 */
        if (withReferences)
        {
            References = sourceDoc.References;
        } else if (sourceDoc.References != null 
            && sourceDoc.References.length > 0 
            && DocumentReference(sourceDoc.References[0]).ReferencedDoc != null)
        {
            References = sourceDoc.References;
        }
 
    }

    public function recalculateTractsCount():void
    {
        TractsCount = TractsList.length;
        
        var tractsAcres:Number = 0;
        
        for each (var tract:Tract in TractsList){
            tractsAcres += tract.CalledAC / ((tract.UnitName == 'Acres') ? 1 : 43560); 
        }
        
        TractsAcres = tractsAcres;
    }

    override public function toString():String
    {
        var stateAbbr:String = DictionaryRegistry.getInstance().getState(State).@StateAbbr;
        return stateAbbr + ', ' + CountyName + ', ' + DocumentPlacement;
    }
 
    public function clone():Document
    {
        var clone:Document = new Document();
        clone.DocID = DocID;
        clone.DocTypeId = DocTypeId;
        clone.County = County;
        clone.State = State;
        clone.DocumentNo = DocumentNo;
        clone.Volume = Volume;
        clone.Page = Page;
        clone.Buyer = Buyer ? Buyer.clone() : null;
        clone.Seller = Seller ? Seller.clone() : null;
        clone.DateFiled = DateFiled;
        clone.DateSigned = DateSigned;
        clone.Created = Created;
        clone.CreatedBy = CreatedBy;
        clone.DocBranchUid = DocBranchUid;
        clone.ImageLink = ImageLink;
        clone.IsActive = IsActive;
        clone.IsPublic = IsPublic;
        clone.IsLoaded = IsLoaded;

        clone.Attachments = [];
        for each (var att:DocumentAttachment in Attachments)
        {
            var attClone:DocumentAttachment = att.clone();
            attClone.DocumentRef = clone;
            
            clone.Attachments.push(attClone);
        }
        
        clone.Tracts = [];
        for each (var tract:Tract in Tracts) 
        {
            var tractClone:Tract = tract.clone();
            tractClone.ParentDocument = clone;
            
            clone.Tracts.push(tractClone);
        }   

        return clone;
    }
    
    public function isAttachmentExists(fileName:String):Boolean 
    {
        for each (var attachment:DocumentAttachment in AttachmentsList) 
        {
            if (attachment.FileRef.FileName == fileName) 
            {
                return true;
            }
        }
        
        return false;
    }
    
    public function isReferenceExists(reference:DocumentReference):Boolean
    {
        for each (var ref:DocumentReference in ReferencesList)
        {
            if (ref.State == reference.State 
                && ref.County == reference.County
                && ref.DocTypeId == reference.DocTypeId
                && (ref.DocumentNo == reference.DocumentNo 
                    || (ref.Volume == reference.Volume && ref.Page == reference.Page))) 
            {
                if (ref.DocumentReferenceId != reference.DocumentReferenceId) 
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    
    public function isRequiredFieldsFilled():Boolean
    {
        if (!isKeyFieldsCompleted())
            return false;
        
        var nullDate:Date = new Date(1901, 01, 01);
            
        if (DateSigned != null && DateSigned > nullDate
            && DateFiled != null && DateFiled > nullDate
            && SellerName != null && SellerName.length > 0
            && BuyerName != null && BuyerName.length > 0) 
        {
            return true;
        } else 
        {
            return false;
        }
    }
    
    public function isKeyFieldsCompleted():Boolean
    {
        if (State > 0 
            && County > 0
            && DocTypeId > 0
            && (DocumentNo != null && DocumentNo.length > 0 
                || (Volume != null && Volume.length > 0 && Page != null && Page.length > 0))) 
        {
            return true;
        } else 
        {
            return false;
        }
    }
}
}
