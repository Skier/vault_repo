package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseExtentionEntity")]
public class LeaseExtention
{
    public var id:int;
    public var term:int = 0;
    public var royalty:Number = 0;
    public var bonusRate:Number = 0;
    public var bonusAmount:Number = 0;
    public var note:String;
    
    public function populate(value:LeaseExtention):void 
    {
    	this.id = value.id;
    	this.term = value.term;
    	this.royalty = value.royalty;
    	this.bonusRate = value.bonusRate;
    	this.bonusAmount = value.bonusAmount;
    	this.note = value.note;
    }

    public function createCopy():LeaseExtention 
    {
    	var result:LeaseExtention = new LeaseExtention();
    	
    	result.term = this.term;
    	result.royalty = this.royalty;
    	result.bonusRate = this.bonusRate;
    	result.bonusAmount = this.bonusAmount;
    	result.note = this.note;
    	
    	return result;
    }
}
}
