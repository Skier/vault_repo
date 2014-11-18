package com.llsvc.domain
{
import flash.events.Event;

import mx.binding.utils.ChangeWatcher;
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseClauseEntity")]
public class LeaseClause
{
	public static const OTHER_TYPE:String = "OTHER";
	public static const OPTION_TO_EXTEND_TYPE:String = "OPT_TO_EXT";
	
    public var id:int;
    public var lease:Lease;
    public var code:String;
    public var name:String;
    public var description:String;
    public var created:Date;
    public var modified:Date;
    public var isActive:Boolean;
    public var details:String;
    public var term:int;
    public var royalty:Number;
    public var bonusRate:Number;
    public var bonusAmount:Number;
    public var alarms:ArrayCollection;
    
    public function get createdStr():String 
    {
    	var result:String = "";
    	if (created != null) 
    	{
    		result += created.month + 1;
    		result += "/";
    		result += created.date;
    		result += "/";
    		result += created.fullYear;
    	}

    	return result;
    }
    public function set createdStr(value:String):void {}
    
    public function get modifiedStr():String 
    {
    	var result:String = "";
    	if (modified != null) 
    	{
    		result += modified.month + 1;
    		result += "/";
    		result += modified.date;
    		result += "/";
    		result += modified.fullYear;
    	}

    	return result;
    }
    public function set modifiedStr(value:String):void {}
    
    public function LeaseClause()
    {
    	this.alarms = new ArrayCollection();
    	this.created = new Date();
    	this.modified = new Date();
    	
    	ChangeWatcher.watch(this, "code", propertyChangeHandler);
    	ChangeWatcher.watch(this, "name", propertyChangeHandler);
    	ChangeWatcher.watch(this, "description", propertyChangeHandler);
    	ChangeWatcher.watch(this, "isActive", propertyChangeHandler);
    	ChangeWatcher.watch(this, "details", propertyChangeHandler);
    	ChangeWatcher.watch(this, "bonusRate", propertyChangeHandler);
    }
    
    private function propertyChangeHandler(event:Event):void 
    {
    	updateDates();
    }
    
    private function updateDates():void 
    {
    	if (created == null)
    		created = new Date();
    	
    	modified = new Date();
    }

    public function populate(value:LeaseClause):void 
    {
	    this.id = value.id;
	    this.code = value.code;
	    this.name = value.name;
	    this.description = value.description;
	    this.created = value.created;
	    this.modified = value.modified;
	    this.isActive = value.isActive;
	    this.details = value.details;
	    this.term = value.term;
	    this.royalty = value.royalty;
	    this.bonusRate = value.bonusRate;
	    this.bonusAmount = value.bonusAmount;
	    
	    this.alarms.removeAll();
	    for each (var o:Object in value.alarms) 
	    {
	    	this.alarms.addItem(o);
	    } 
    }

    public function createCopy():LeaseClause 
    {
    	var result:LeaseClause = new LeaseClause();
    	
	    result.id = this.id;
	    result.code = this.code;
	    result.name = this.name;
	    result.description = this.description;
	    result.created = this.created;
	    result.modified = this.modified;
	    result.isActive = this.isActive;
	    result.details = this.details;
	    result.term = this.term;
	    result.royalty = this.royalty;
	    result.bonusRate = this.bonusRate;
	    result.bonusAmount = this.bonusAmount;
	    
	    for each (var o:Object in this.alarms) 
	    {
	    	result.alarms.addItem(o);
	    } 

	    return result;
    }
    
    public static function getTypes():ArrayCollection 
    {
    	var result:ArrayCollection = new ArrayCollection();
    	
    	result.addItem({value:"DEPTH", label:"Depth Clause"});
    	result.addItem({value:"DAMAGE", label:"Damage"});
    	result.addItem({value:"PUGH", label:"Horizontal Pugh Clause"});
    	result.addItem({value:"SHUT_IN_GAS", label:"Vertical Pugh Clause"});
    	result.addItem({value:"TAKE_GAS_ROY", label:"Royalty in Kind"});
    	result.addItem({value:"SURFACE", label:"Surface Use"});
    	result.addItem({value:"CONT_DRILL", label:"Cont Drilling"});
    	result.addItem({value:"FAV_NAT", label:"Favored Nations"});
    	result.addItem({value:OPTION_TO_EXTEND_TYPE, label:"Option to Extend"});
    	result.addItem({value:"ASSIGNMENT", label:"Assignment"});
    	result.addItem({value:"PROD_PAYM", label:"Prod Payment"});
    	result.addItem({value:"POOL_PROV", label:"Pooling Provision"});
    	result.addItem({value:"MIN_ROY_PAY", label:"Min Royalty Payment"});
    	result.addItem({value:"RENEWAL_OPT", label:"Renewal Option"});
    	result.addItem({value:"HBP", label:"HBP"});
    	result.addItem({value:"SPC_PROV", label:"Spacing Provision"});
    	result.addItem({value:"LESSER_INT", label:"Lesser Interest"});
    	result.addItem({value:"REWORK_DAYS", label:"Rework Days"});
    	result.addItem({value:"COUNTERPARTS", label:"Counterparts"});
    	result.addItem({value:OTHER_TYPE, label:"Other"});
        
    	return result;
    }

}
}
