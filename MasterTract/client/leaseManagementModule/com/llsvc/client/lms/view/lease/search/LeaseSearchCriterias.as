package com.llsvc.client.lms.view.lease.search
{
	import com.llsvc.client.lms.LocalCash;
	import com.llsvc.domain.LeaseTract;
	import com.llsvc.domain.Project;
	
	import mx.binding.utils.ChangeWatcher;
	import mx.collections.ArrayCollection;
	
[Bindable]
[RemoteClass(alias="com.llsvc.server.doc.LeaseSearchCriterias")]
public class LeaseSearchCriterias
{
    public var leaseNo:String;
    public var leaseName:String;
    public var projectId:String;
    public var recordInfo:String;
    public var expDate:DateRange;
    public var grossAc:NumberRange;
    public var netAc:NumberRange;
    public var interest:NumberRange;
    public var leaseBurden:NumberRange;
    public var leaseNri:NumberRange;
    public var wi:NumberRange;
    public var additionalBurden:NumberRange;
    public var nri:NumberRange;
    public var net:NumberRange;
    
    public function set projectName(value:String):void {}
    public function get projectName():String 
    {
    	var project:Project = LocalCash.getInstance().getProjectById(int(Number(projectId)));
    	if (project == null)
    		return "ANY";
    	else 
    		return project.name;
    }
    
    public var tracts:ArrayCollection;
    
    public function LeaseSearchCriterias()
    {
    	tracts = new ArrayCollection();
    	expDate = new DateRange();
    	grossAc = new NumberRange();
    	netAc = new NumberRange();
    	interest = new NumberRange();
    	leaseBurden = new NumberRange();
    	leaseNri = new NumberRange();
    	wi = new NumberRange();
    	additionalBurden = new NumberRange();
    	nri = new NumberRange();
    	net = new NumberRange();
    	
    	ChangeWatcher.watch(this, "projectId", function(ev:Event):void {projectName = null;}); 
    }
    
    public function copyForQuery():LeaseSearchCriterias 
    {
    	var result:LeaseSearchCriterias = new LeaseSearchCriterias();
    	
    	result.leaseNo = this.leaseNo;

    	if (this.leaseName != null && this.leaseName.length > 0)
    		result.leaseName = "%" + this.leaseName.toUpperCase() + "%";
    	else 
    		result.leaseName = null;
    	
    	result.projectId = this.projectId;

    	if (this.recordInfo != null && this.recordInfo.length > 0)
	    	result.recordInfo = "%" + this.recordInfo + "%";
    	else 
    		result.recordInfo = null;

	    result.expDate = this.expDate;
	    result.grossAc = this.grossAc;
	    result.netAc = this.netAc;
	    result.interest = this.interest;
	    result.leaseBurden = this.leaseBurden;
	    result.leaseNri = this.leaseNri;
	    result.wi = this.wi;
	    result.additionalBurden = this.additionalBurden;
	    result.nri = this.nri;
	    result.net = this.net;
	    
	    for each (var t:TractSearchCriteria in tracts) 
	    {
	    	result.tracts.addItem(t.copyForQuery());
	    }
    	
    	return result;
    }
    
    public function accept(tract:LeaseTract):Boolean 
    {
    	if (this.tracts.length == 0)
    		return true;
    	
    	for each (var tractCriteria:TractSearchCriteria in this.tracts) 
    	{
    		if (tractCriteria.accept(tract))
    			return true;
    	}
    	
    	return false;
    } 
}
}