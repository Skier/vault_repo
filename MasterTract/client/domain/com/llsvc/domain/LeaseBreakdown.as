package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseBreakdownEntity")]
public class LeaseBreakdown
{
    public var id:int;
    public var interest:Number = 0;
    public var actor:DocumentActor;
    public var tract:LeaseTract;
    public var fromDepth:String;
    public var toDepth:String;
    public var product:String;
    public var formation:String;
    
    public function get actorName():String 
    {
        if (this.actor != null) 
        {
            return this.actor.name;
        } else 
        {
            return "actor N/A"
        }
    }
    
    public function set actorName(value:String):void 
    {
        if (this.actor != null) 
        {
            this.actor.name = value;
        }
    }
    
    public function get interestStr():String 
    {
    	return (interest * 100).toFixed(2) + "%";
    }

    public function createCopy():LeaseBreakdown 
    {
    	var result:LeaseBreakdown = new LeaseBreakdown();
    	
    	result.interest = this.interest;
    	result.actor = this.actor.createCopy();
    	result.fromDepth = this.fromDepth;
    	result.toDepth = this.toDepth;
    	result.product = this.product;
    	result.formation = this.formation;
    	
    	return result;
    }
}
}
