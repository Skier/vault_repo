package com.llsvc.domain
{
import flash.events.EventDispatcher;

import mx.binding.utils.ChangeWatcher;
import mx.collections.ArrayCollection;
import mx.events.PropertyChangeEvent;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseTractEntity")]
public class LeaseTract extends EventDispatcher
{
    public var id:int;
    public var township:String = "";
    public var range:String= "";
    public var section:String= "";
    public var tract:String= "";
    public var grossAcres:Number = 0;
    public var netAcres:Number = 0;
    public var lease:Lease;
    public var note:String;
    public var qqs:ArrayCollection;
    public var breakdown:ArrayCollection;
    public var nri:Number = 0;
    public var cwi:Number = 0;
    public var burden:Number = 0;
    public var cnri:Number = 0;
    
    public var leaseInterest:Number = 0;
    public var leaseBurden:Number = 0;
    public var cNetAcres:Number = 0;

    public var units:String;
    public var state:State;
    public var county:County;
    public var isSurfaceOwner:Boolean;
    public var surfaceOwnerContact:String = "";
    
    public var isSelected:Boolean = false;
    
    public function LeaseTract()
    {
        qqs = new ArrayCollection();
        breakdown = new ArrayCollection();
        
        ChangeWatcher.watch(this, "grossAcres", propertyChangeHandler);
        ChangeWatcher.watch(this, "netAcres", propertyChangeHandler);
        ChangeWatcher.watch(this, "nri", propertyChangeHandler);
        ChangeWatcher.watch(this, "cwi", propertyChangeHandler);
        ChangeWatcher.watch(this, "burden", propertyChangeHandler);
        ChangeWatcher.watch(this, "cnri", propertyChangeHandler);
        ChangeWatcher.watch(this, "leaseInterest", propertyChangeHandler);
        ChangeWatcher.watch(this, "leaseBurden", propertyChangeHandler);
        ChangeWatcher.watch(this, "cNetAcres", propertyChangeHandler);
    }
    
    public var isProcessing:Boolean = false;
    public var isProcessed:Boolean = false;

    private function propertyChangeHandler(e:PropertyChangeEvent):void 
    {
        if (lease) {
            lease.isDirty = true;
            lease.updateTractsSummary();
        }
        
        cNetAcres = netAcres * cwi;
        cnri = 1 - (leaseBurden + burden);
        
        if (e.property == "leaseBurden")
            nri = 1 - leaseBurden; 
    }
    
    public function get townshipRangeStr():String 
    {
        var tsa:Array = township.split(" ");
        var ra:Array = range.split(" ");
        
        return ("T" + tsa[0] + tsa[2] + " R" + ra[0] + ra[2] + " PM" + ra[4]);
    }
    
    public function get townshipStr():String 
    {
        var arr:Array = township.split(" ");
        if (arr.length > 0) {
            return arr[0] as String;
        } else {
            return "";
        }
    }

    public function get townshipDirStr():String 
    {
        var arr:Array = township.split(" ");
        if (arr.length > 2) {
            return arr[2] as String;
        } else {
            return "";
        }
    }

    public function get rangeStr():String 
    {
        var arr:Array = range.split(" ");
        if (arr.length > 0) {
            return arr[0] as String;
        } else {
            return "";
        }
    }

    public function get rangeDirStr():String 
    {
        var arr:Array = range.split(" ");
        if (arr.length > 2) {
            return arr[2] as String;
        } else {
            return "";
        }
    }

    public function get meridianStr():String 
    {
        var arr:Array = range.split(" ");
        if (arr.length > 4) {
            return arr[4] as String;
        } else {
            return "";
        }
    }
    
    public function get sectionStr():String 
    {
        var sec:int = int(Number(section));
        if (sec < 10) {
            return ("0" + sec.toString());
        } else {
            return sec.toString();
        }
    }

    public function get secInt():int 
    {
        return int(Number(section));
    }

    public function get nriStr():String 
    {
        return (nri * 100).toFixed(2) + "%";
    }
//    public function set nriStr(value:String):void 
//    {
//      if (!isNaN(Number(value))) nri = Number(value) else nri = 0;
//    }
    
    public function get cwiStr():String 
    {
        return (cwi * 100).toFixed(2) + "%";
    }
//    public function set cwiStr(value:String):void 
//    {
//      if (!isNaN(Number(value))) cwi = Number(value) else cwi = 0;
//    }
    
    public function get burdenStr():String 
    {
        return (burden * 100).toFixed(2) + "%";
    }
//    public function set burdenStr(value:String):void 
//    {
//      if (!isNaN(Number(value))) burden = Number(value) else burden = 0;
//    }
    
    public function get cnriStr():String 
    {
        return (cnri * 100).toFixed(2) + "%";
    }
//    public function set cnriStr(value:String):void 
//    {
//      if (!isNaN(Number(value))) cnri = Number(value) else cnri = 0;
//    }
    
    public function get leaseInterestStr():String 
    {
        return (leaseInterest * 100).toFixed(2) + "%";
    }

    public function get leaseBurdenStr():String 
    {
        return (leaseBurden * 100).toFixed(2) + "%";
    }

    public function get tractType():String 
    {
        return "PLSS";
    }

    public function createCopy():LeaseTract 
    {
        var result:LeaseTract = new LeaseTract();
        
        result.township = this.township;
        result.range = this.range;
        result.section = this.section;
        result.tract = this.tract;
        result.grossAcres = this.grossAcres;
        result.netAcres = this.netAcres;
        result.note = this.note;
        result.nri = this.nri;
        result.cwi = this.cwi;
        result.burden = this.burden;
        result.cnri = this.cnri;
        result.leaseInterest = this.leaseInterest;
        result.leaseBurden = this.leaseBurden;
        result.cNetAcres = this.cNetAcres;
        result.units = this.units;
        result.isSurfaceOwner = this.isSurfaceOwner;
        result.surfaceOwnerContact = this.surfaceOwnerContact;
/*         
        for each (var lb:LeaseBreakdown in breakdown) 
        {
            var bdown:LeaseBreakdown = lb.createCopy();
            bdown.tract = result; 
            result.breakdown.addItem(bdown);
        }
 */        
        for each (var q:LeaseTractQQ in qqs) 
        {
            var tractQQ:LeaseTractQQ = q.createCopy();
            tractQQ.leaseTract = result;
            result.qqs.addItem(tractQQ);
        }
        
        return result;
    }
}
}
