package com.llsvc.domain
{
	import mx.collections.ArrayCollection;
	
[Bindable]
public class DocumentAssignment
{
	public var royalty:Number = 0;
	public var isFullLeaseSet:Boolean = false;
	public var assignmentDate:Date;
	public var effectiveDate:Date;
	public var depth:String;
	public var burdens:String; 
	
	public var document:Document;
	public var leases:ArrayCollection;
	public var tracts:ArrayCollection;

	public function DocumentAssignment()
	{
		document = new Document();
		leases = new ArrayCollection();
		tracts = new ArrayCollection();
	}

}
}