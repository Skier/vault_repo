package com.llsvc.domain
{
[Bindable]
public class DocumentCorrespondence
{
	
	public var correspDate:Date;
	public var correspFrom:String;
	public var correspTo:String;
	public var description:String;
	
	public var document:Document;
	
	public function DocumentCorrespondence()
	{
		document = new Document();
	}

}
}