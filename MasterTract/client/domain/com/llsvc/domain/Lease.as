package com.llsvc.domain
{
import flash.events.EventDispatcher;

import mx.binding.utils.ChangeWatcher;
import mx.collections.ArrayCollection;
import mx.formatters.CurrencyFormatter;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseEntity")]
public class Lease extends EventDispatcher
{
    public var document:Document;
    public var prospectName:String;
    public var leaseName:String;
    public var leaseDate:Date;
    public var effectiveDate:Date;
    public var term:int;
    public var isPaidUp:Boolean;
    public var royalty:Number = 0;
    public var royaltyInput:String;
    public var bonusRate:Number = 0;
    public var bonusAmount:Number = 0;
    public var grossAcres:Number = 0;
    public var netAcres:Number = 0;
    public var note:String;
    public var leaseNum:int;
    public var locationId:String;

    public var vet:Boolean;
    public var options:String;
    public var termStatus:String;

	public var extention:LeaseExtention;

    public var clauses:ArrayCollection;
    public var tracts:ArrayCollection;
    public var alarms:ArrayCollection;
    
    public var assignment:LeaseAssignment;
    
    public var rentDueDate:Date;
    
    public var isDirty:Boolean = false;

    public var isProcessing:Boolean = false;
    public var isProcessed:Boolean = false;

    public var isLoading:Boolean = false;
    public var isLoaded:Boolean = true;

	public var localMessage:String = "";
    
    private var cf:CurrencyFormatter;

    public function Lease()
    {
        this.document = new Document();
        this.document.documentType = new DocumentType();
        this.document.documentType.giverRole = "Lessor";
        this.document.documentType.receiverRole = "Lessee";
        this.document.documentType.name = "Lease";

		this.extention = new LeaseExtention();
        this.clauses = new ArrayCollection();
        this.tracts = new ArrayCollection();
        this.alarms = new ArrayCollection();
        
        cf = new CurrencyFormatter();
        cf.precision = 2;
        
        ChangeWatcher.watch(this, "isComplete", isCompleteChangeHandler);
        ChangeWatcher.watch(this, "royalty", royaltyChangeHandler);
    }
    
    private function isCompleteChangeHandler(event:*):void 
    {
    	isDirty = true;
    }
    
    private function royaltyChangeHandler(event:*):void 
    {
    	for each (var t:LeaseTract in tracts) 
    	{
    		t.leaseBurden = this.royalty;
    	}
    }
    
    public function get leasorStr():String 
    {
    	return document.giversStr;
    }
    
    public function get leaseeStr():String 
    {
        var result:String = "";
        
        for each (var actor:DocumentActor in document.actors) 
        {
        	if (!actor.isGiver) 
        	{
	            result += (result.length > 0 ? ", " : "");
	            result += (actor.name);
        	}
        }
        
        return result;
    }
    
    public function get recordsStr():String 
    {
    	return this.document.recordsStr;
    }
    
    public function get recordsAllStr():String 
    {
    	return this.document.recordsAllStr;
    }
    
    public function get leaseDateStr():String 
    {
        return this.leaseDate.toDateString();
    }
    
    public function get leaseDateExcelStr():String 
    {
        return (leaseDate.fullYear.toString() + "-" + (leaseDate.month + 1).toString() + "-" + leaseDate.date.toString());
    }
    
    public function get effectiveDateStr():String 
    {
        return this.effectiveDate.toDateString();
    }
    
    public function get termStr():String 
    {
        return this.term.toString() + " M";
    }
    
    public function get royaltyStr():String 
    {
        return this.royalty.toString();
    }
    
    public function get bonusRateStr():String 
    {
        return cf.format(this.bonusRate);
//        return "$" + this.bonusRate.toFixed(2);
    }
    
    public function get bonusAmountStr():String 
    {
        return cf.format(this.bonusAmount);
//        return "$" + this.bonusAmount.toFixed(2);
    }
    
    public function get grossAcresStr():String 
    {
        return this.grossAcres.toString();
    }
    
    public function get netAcresStr():String 
    {
        return this.netAcres.toString();
    }
    
    public function get expirationDateStr():String 
    {
        return addMonths(this.effectiveDate, term).toDateString();78
    }
    
    public function get expirationDateExcelStr():String 
    {
    	var expDate:Date = addMonths(this.effectiveDate, term);
    	
        return (expDate.fullYear.toString() + "-" + (expDate.month + 1).toString() + "-" + expDate.date.toString());
    }
    
	public function get isComplete():Boolean 
	{
		return (document.documentStatus.id == DocumentStatus.COMPLETE_ID);
	}
	
	public function set isComplete(value:Boolean):void 
	{
		document.setComplete(value);
	}
//------------	
	
	public var tractsInterestStr:String; 
	public var tractsBurdenStr:String; 
	public var tractsNriStr:String; 
	public var tractsCwiStr:String; 
	public var tractsAddBurdensStr:String; 
	public var tractsCnriStr:String; 
	public var tractsCNetAcresStr:String; 
	public var tractsGrossAcresStr:String; 
	public var tractsNetAcresStr:String; 
	
	public function updateTractsSummary():void 
	{
		var tractsGrossAcresSum:Number = 0; 
		var tractsNetAcresSum:Number = 0; 
		var tractsBurdenSum:Number = 0; 
		var tractsNriSum:Number = 0; 
		var tractsCwiSum:Number = 0; 
		var tractsAddBurdensSum:Number = 0; 
		var tractsCnriSum:Number = 0; 
		var tractsCNetAcresSum:Number = 0; 

		for each (var tract:LeaseTract in tracts) 
		{
			tractsGrossAcresSum += tract.grossAcres;
			tractsNetAcresSum += tract.netAcres;
			tractsBurdenSum += tract.leaseBurden;
			tractsNriSum += tract.nri;
			tractsCwiSum += tract.cwi;
			tractsAddBurdensSum += tract.burden;
			tractsCnriSum += tract.cnri;
			tractsCNetAcresSum += tract.cNetAcres;
		}
		
		tractsGrossAcresStr = tractsGrossAcresSum.toFixed(2);
		grossAcres = tractsGrossAcresSum;
		tractsNetAcresStr = tractsNetAcresSum.toFixed(2);
		netAcres = tractsNetAcresSum;
		
		if (tracts.length > 0) 
		{
			tractsInterestStr = (grossAcres > 0) ? (netAcres/grossAcres * 100).toFixed(2) + "%" : "";
			//tractsInterestStr = ((tractsInterestSum / tracts.length) * 100).toFixed(2) + "%";
			tractsBurdenStr = ((tractsBurdenSum / tracts.length) * 100).toFixed(2) + "%";
			tractsNriStr = ((tractsNriSum / tracts.length) * 100).toFixed(2) + "%";
			tractsCwiStr = ((tractsCwiSum / tracts.length) * 100).toFixed(2) + "%";
			tractsAddBurdensStr = ((tractsAddBurdensSum / tracts.length) * 100).toFixed(2) + "%";
			tractsCnriStr = ((tractsCnriSum / tracts.length) * 100).toFixed(2) + "%";
		}
		
		tractsCNetAcresStr = tractsCNetAcresSum.toFixed(2);
	} 
	
    public function populate(value:Lease):void 
    {
        this.prospectName = value.prospectName;
        this.leaseName = value.leaseName;
        this.leaseDate = value.leaseDate;
        this.effectiveDate = value.effectiveDate;
        this.term = value.term;
        this.isPaidUp = value.isPaidUp;
        this.royalty = value.royalty;
        this.royaltyInput = value.royaltyInput;
        this.bonusRate = value.bonusRate;
        this.bonusAmount = value.bonusAmount;
        this.grossAcres = value.grossAcres;
        this.netAcres = value.netAcres;
        this.vet = value.vet;
        this.options = value.options;
        this.termStatus = value.termStatus;
        
        if (value.document == null) {
            this.document = null;
        } else {
            this.document.populate(value.document);
        }
        
        this.clauses.removeAll();
        for each (var clause:LeaseClause in value.clauses) 
        {
        	var cl:LeaseClause = new LeaseClause();
        	cl.populate(clause);
        	this.clauses.addItem(cl);
        }
        
        tracts.removeAll();
        for each (var lb:LeaseTract in value.tracts) {
            lb.lease = this;
       		lb.leaseBurden = this.royalty;
            tracts.addItem(lb);
        }
        tracts.dispatchEvent(new Event("tractsReloaded"));
        
        this.alarms.removeAll();
        for each (var alarm:LeaseAlarm in value.alarms) 
        {
        	this.alarms.addItem(alarm);
        }
        
        if (value.assignment == null) {
            this.assignment = null;
        } else {
            this.assignment.populate(value.assignment);
        }
        
        if (this.leaseName == null || this.leaseName.length == 0) 
        {
            this.leaseName = this.document.giversStr;
        }
        
        this.isDirty = false;

        this.updateTractsSummary();
    }
    
    public function createCopy():Lease 
    {
        var result:Lease = new Lease();
        
        result.prospectName = this.prospectName;
        result.leaseName = this.leaseName;
        result.leaseDate = this.leaseDate;
        result.effectiveDate = this.effectiveDate;
        result.term = this.term;
        result.isPaidUp = this.isPaidUp;
        result.royalty = this.royalty;
        result.royaltyInput = this.royaltyInput;
        result.bonusRate = this.bonusRate;
        result.bonusAmount = this.bonusAmount;
        result.grossAcres = this.grossAcres;
        result.netAcres = this.netAcres;
        result.vet = this.vet;
        result.options = this.options;
        result.termStatus = this.termStatus;
        
        if (this.document == null) {
            result.document = null;
        } else {
            result.document = this.document.createCopy();
        }
        
        for each (var clause:LeaseClause in this.clauses) 
        {
        	var cl:LeaseClause = clause.createCopy();
        	cl.lease = result;
        	result.clauses.addItem(cl);
        }
        
        for each (var t:LeaseTract in this.tracts) {
            var tract:LeaseTract = t.createCopy();
            tract.lease = result;
            result.tracts.addItem(tract);
        }
        result.tracts.dispatchEvent(new Event("tractsReloaded"));

        result.leaseName = this.leaseName + " (copy)";
        
        result.isDirty = false;
        result.updateTractsSummary();
        
        return result;
    }
    
    private function addMonths(d:Date, x:int):Date {
        var y:Number = Math.floor(x / 12); x -= 12*y;
        var m:Number = d.month + x;
        if (m > 11) {
            y += 1;
            m -= 12;
        } else if (m < 0) {
            y -= 1;
            m += 12;
        }
        return new Date(d.fullYear+y, m, d.date, d.hours, d.minutes, d.seconds, d.milliseconds);
    }
}
}
