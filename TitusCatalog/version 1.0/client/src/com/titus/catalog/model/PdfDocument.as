package com.titus.catalog.model
{
	import mx.collections.ArrayCollection;
	
[Bindable]
[RemoteClass(alias="Titus.ECatalog.Entity.DocumentDataObject")]
public class PdfDocument
{
	
	public function set PageNumber(value:int):void {
		pageNumber = value;
	}
	
	public var pageNumber:int;
	
	public function set DocumentId(value:int):void {
		id = value;
	}
	
	public var id:int;
	
	public var pageCount:int;
	
	public var width:Number;
	
	public var height:Number;
	
	public function set Pages(value:Array):void {
		pages.removeAll();
		for each (var page:PdfPage in value) {
			pages.addItem(page);
		}
		
		pageCount = pages.length;
		
		if (0 < pageCount) {
			var firstPage:PdfPage = PdfPage(pages.getItemAt(0));
			width = firstPage.width;
			height = firstPage.height;
		}
	}
	
	public var pages:ArrayCollection;
	
	public function PdfDocument()
	{
		pages = new ArrayCollection();
	}

}

}
