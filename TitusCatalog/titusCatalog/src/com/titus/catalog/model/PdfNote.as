package com.titus.catalog.model
{
	
public class PdfNote
{
	
	public var noteId:int;
	public function set NoteId(value:int):void {
		noteId = value;
	}
	public function get NoteId():int {
		return noteId;
	}
	
	public var userId:int;
	public function set UserId(value:int):void {
		userId = value;
	}
	public function get UserId():int {
		return userId;
	}
	
	public var pageId:int;
	public function set DocumentPageId(value:int):void {
		pageId = value;
	}
	public function get DocumentPageId():int {
		return pageId;
	}
	
	public var top:int;
	public function set Top(value:int):void {
		top = value;
	}
	public function get Top():int {
		return top;
	}
	
	public var left:int;
	public function set Left(value:int):void {
		left = value;
	}
	public function get Left():int {
		return left;
	}
	
	public var width:int;
	public function set Width(value:int):void {
		width = value;
	}
	public function get Width():int {
		return width;
	}
	
	public var height:int;
	public function set Height(value:int):void {
		height = value;
	}
	public function get Height():int {
		return height;
	}
	
	public var text:String;
	public function set NoteText(value:String):void {
		text = value;
	}
	public function get NoteText():String {
		return text;
	}
	
}
	
}
