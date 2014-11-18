package com.llsvc.domain
{
[Bindable]
public class DocumentAgreement
{
	public var signDate:Date;
	public var effectiveDate:Date;
	public var note:String;
	
	public var document:Document;
	
	public function DocumentAgreement()
	{
		document = new Document();
	}

}
}