package truetract.domain
{
	import mx.formatters.DateFormatter;
	import truetract.domain.mementos.DocumentReferenceMemento;
	
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentReferenceInfo")]
public class DocumentReference
{
    public var DocumentReferenceId:int;
    public var DocumentId:int;
    public var ReferenceId:int;
    public var Description:String;

    public var State:int;
    public var County:int;
    public var DocTypeId:int;
    public var DocumentNo:String;
    public var Volume:String;
    public var Page:String;

    public var ParentDocumentRef:Document;
    public var ReferencedDoc:Document;


    public function get DocumentTypeName():String
    {
        return ReferencedDoc ? ReferencedDoc.DocumentTypeName : "n/a";
    }

    public function get DateSigned():String
    {
        return ReferencedDoc ? getDateFormater().format(ReferencedDoc.DateSigned) : "n/a";
    }

    public function get DateFiledDisplayValue():String
    {
        return ReferencedDoc ? getDateFormater().format(ReferencedDoc.DateFiled) : "n/a";
    }

    public function get DateFiledSortValue():Number
    {
        return ReferencedDoc ? ReferencedDoc.DateFiled.getTime() : NaN;
    }

    public function get SellerName():String
    {
        return ReferencedDoc ? ReferencedDoc.SellerName : "n/a";
    }

    public function get BuyerName():String
    {
        return ReferencedDoc ? ReferencedDoc.BuyerName : "n/a";
    }

    private var _df:DateFormatter;
    private function getDateFormater():DateFormatter
    {
        if (!_df)
        {
            _df = new DateFormatter();
            _df.formatString = "MMM DD YYYY";
        }
        return _df;
    };
    
    public function getMemento():Object
    {
        var memento:DocumentReferenceMemento = new DocumentReferenceMemento();

		memento.documentReferenceId = DocumentReferenceId;
		memento.documentId = DocumentId;
		memento.referenceId = ReferenceId;
		memento.description = Description;
		memento.state = State;
		memento.county = County;
		memento.docTypeId = DocTypeId;
		memento.documentNo = DocumentNo;
		memento.volume = Volume;
		memento.page = Page;
		
        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:DocumentReferenceMemento = DocumentReferenceMemento(value);

		DocumentReferenceId = memento.documentReferenceId;
		DocumentId = memento.documentId;
		ReferenceId = memento.referenceId;
		Description = memento.description;
		State = memento.state;
		County = memento.county;
		DocTypeId = memento.docTypeId;
		DocumentNo = memento.documentNo;
		Volume = memento.volume;
		Page = memento.page;
    }

	public function updateLocalFields():void 
	{
		if (ReferencedDoc == null)
			return;
			
		State = ReferencedDoc.State;
		County = ReferencedDoc.County;
		DocTypeId = ReferencedDoc.DocTypeId;
		DocumentNo = ReferencedDoc.DocumentNo;
		Volume = ReferencedDoc.Volume;
		Page = ReferencedDoc.Page;
	}
}
}