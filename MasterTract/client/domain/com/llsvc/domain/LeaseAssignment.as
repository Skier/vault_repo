package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseAssignmentEntity")]
public class LeaseAssignment
{
    public var document:Document;
    public var royalty:Number;
    public var assignDate:Date;
    public var effectiveDate:Date;
    public var isFullLeaseSet:Boolean;

    public var leaseSet:ArrayCollection;
    
    public function LeaseAssignment()
    {
	    this.document = new Document();
	    this.leaseSet = new ArrayCollection();
    }
    
    public function populate(value:LeaseAssignment, cascade:Boolean = false):void
    {
    	this.royalty = value.royalty;
    	this.assignDate = value.assignDate;
    	this.effectiveDate = value.effectiveDate;
    	this.isFullLeaseSet = value.isFullLeaseSet;
    	
    	if (value.document == null){
	    	this.document = null;
    	} else {
	    	this.document.populate(value.document);
    	}
    	
    	if (cascade && value.leaseSet != null) {
    		this.leaseSet.removeAll();
    		for each (var lease:Lease in value.leaseSet){
    			this.leaseSet.addItem(lease);
    		}
    	}
    }
}
}
