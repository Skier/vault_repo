package com.titus.catalog.model
{
	import com.titus.catalog.view.pdf.BitmapPdfPage;
	
	import flash.display.BitmapData;
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	
[Bindable]
[RemoteClass(alias="Titus.ECatalog.Entity.SectionPageDataObject")]
public class PdfPage extends EventDispatcher
{
	
	public function set SectionPageId(value:int):void {
		id = value;
	}
	
	public var id:int;

	public function set SectionId(value:int):void {
		documentId = value;
	}
	
	public var documentId:int;
	
	public function set SectionPageNumber(value:int):void {
		number = value;
	}
	
	public var number:int;
	
	public function set ScreenshotURL(value:String):void {
		imageUrl = value;
	}
	
	public var imageUrl:String;
	
	public var image:BitmapData;
	
	public var isLoaded:Boolean;
	
	public var isLoading:Boolean;
	
	public function set Width(value:int):void {
		width = value;
	}
	
	public var width:int;
	
	public function set Height(value:int):void {
		height = value;
	}
	
	public var height:int;
	
	public function set Notes(value:Array):void {
		notes.removeAll();
		for each (var noteInfo:PdfNote in value) {
			notes.addItem(noteInfo);
		}
	}
	
	public var notes:ArrayCollection;
	
	public var bitmap:BitmapPdfPage;
		
	public function PdfPage()
	{
		isLoaded = false;
		isLoading = false;
		
		notes = new ArrayCollection();
	}

}

}
