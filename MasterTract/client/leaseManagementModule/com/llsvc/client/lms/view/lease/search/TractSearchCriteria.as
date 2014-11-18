package com.llsvc.client.lms.view.lease.search
{
import com.llsvc.domain.LeaseTract;
	
[Bindable]
[RemoteClass(alias="com.llsvc.server.doc.TractSearchCriteria")]
public class TractSearchCriteria
{
	public var township:String;
	public var range:String;
	public var section:String;
	
	public var stateId:String;
	public var stateStr:String = "";
	public var countyId:String;
	public var countyStr:String = "";
	
	public var twn:String = "";
	public var twnDir:String = "";
	public var rng:String = "";
	public var rngDir:String = "";
	public var sec:String = "";
	public var pm:String = "";
	public var pmDescr:String = "";
	
	
	public function TractSearchCriteria()
	{
		township = "";
		range = "";
		section = "";
	}
    
    public function copyForQuery():TractSearchCriteria 
    {
    	var result:TractSearchCriteria = new TractSearchCriteria();
    	
    	if (this.twn != null && this.twn.length > 0)
    		result.township = "%" + this.twn.toUpperCase() + "%";
    	else 
    		result.township = "";

    	if (this.twnDir != null && this.twnDir.length > 0)
    		if (result.township.length > 0)
    			result.township += (this.twnDir.toUpperCase() + "%");
    		else 
    			result.township = "%" + (this.twnDir.toUpperCase() + "%");
    	
    	if (result.township.length == 0)
    		result.township = null;
    	

    	if (this.rng != null && this.rng.length > 0)
    		result.range = "%" + this.rng.toUpperCase() + "%";
    	else 
    		result.range = "";

    	if (this.rngDir != null && this.rngDir.length > 0)
    		if (result.range.length > 0)
    			result.range += (this.rngDir.toUpperCase() + "%");
    		else 
    			result.range = "%" + (this.rngDir.toUpperCase() + "%");
    	
    	if (this.pm != null && this.pm.length > 0)
    		if (result.range.length > 0)
    			result.range += ("PM " + this.pm.toUpperCase() + "%");
    		else 
    			result.range = "%" + ("PM " + this.pm.toUpperCase() + "%");
    	
    	if (result.range.length == 0)
    		result.range = null;

    	if (this.sec != null && this.sec.length > 0)
    		result.section = "%" + this.sec.toUpperCase() + "%";
    	else 
    		result.section = null;

		if (this.stateId != null && this.stateId.length > 0)
			result.stateId = this.stateId;
		else 
			result.stateId = null;

		if (this.countyId != null && this.countyId.length > 0)
			result.countyId = this.countyId;
		else 
			result.countyId = null;

    	return result;
    }

    public function copy():TractSearchCriteria 
    {
    	var result:TractSearchCriteria = new TractSearchCriteria();
    	
    	result.twn = this.twn;
    	result.twnDir = this.twnDir;
    	result.rng = this.rng;
    	result.rngDir = this.rngDir;
    	result.sec = this.sec;
    	result.pm = this.pm;
    	result.pmDescr = this.pmDescr;

    	return result;
    }
    
    public function accept(tract:LeaseTract):Boolean 
    {
    	if (tract == null)
    		return false;
    		
    	var townshipAccepted:Boolean = false;
    	var townshipDirAccepted:Boolean = false;
    	var rangeAccepted:Boolean = false;
    	var rangeDirAccepted:Boolean = false;
    	var pmAccepted:Boolean = false;
    	var sectionAccepted:Boolean = false;
    	var stateAccepted:Boolean = false;
    	var countyAccepted:Boolean = false;
    	
    	if (this.twn != null && this.twn.length > 0) 
    	{
    		if (tract.townshipStr.search(this.twn) > -1)
    			townshipAccepted = true;
    		else 
    			townshipAccepted = false;
    	} else 
    	{
    		townshipAccepted = true;
    	}
    	
    	if (this.twnDir != null && this.twnDir.length > 0) 
    	{
    		if (tract.townshipDirStr.search(this.twnDir) > -1)
    			townshipDirAccepted = true;
    		else 
    			townshipDirAccepted = false;
    	} else 
    	{
    		townshipDirAccepted = true;
    	}
    	
    	if (this.rng != null && this.rng.length > 0) 
    	{
    		if (tract.rangeStr.search(this.rng) > -1)
    			rangeAccepted = true;
    		else 
    			rangeAccepted = false;
    	} else 
    	{
    		rangeAccepted = true;
    	}
    	
    	if (this.rngDir != null && this.rngDir.length > 0) 
    	{
    		if (tract.rangeDirStr.search(this.rngDir) > -1)
    			rangeDirAccepted = true;
    		else 
    			rangeDirAccepted = false;
    	} else 
    	{
    		rangeDirAccepted = true;
    	}
    	
    	if (this.pm != null && this.pm.length > 0) 
    	{
    		if (tract.meridianStr.search(this.pm) > -1)
    			pmAccepted = true;
    		else 
    			pmAccepted = false;
    	} else 
    	{
    		pmAccepted = true;
    	}
    	
    	if (this.sec != null && this.sec.length > 0) 
    	{
    		if (tract.sectionStr.search(this.sec) > -1)
    			sectionAccepted = true;
    		else 
    			sectionAccepted = false;
    	} else 
    	{
    		sectionAccepted = true;
    	}
    	
    	if (this.stateId != null && this.stateId.length > 0) 
    	{
    		if (tract.state != null && tract.state.id.toString() == this.stateId)
    			stateAccepted = true;
    		else 
    			stateAccepted = false;
    	} else 
    	{
    		stateAccepted = true;
    	}
    	
    	if (this.countyId != null && this.countyId.length > 0) 
    	{
    		if (tract.county != null && tract.county.id.toString() == this.countyId)
    			countyAccepted = true;
    		else 
    			countyAccepted = false;
    	} else 
    	{
    		countyAccepted = true;
    	}
   	
	    if (townshipAccepted 
	    	&& townshipDirAccepted 
	    	&& rangeAccepted 
	    	&& rangeDirAccepted 
	    	&& pmAccepted 
	    	&& sectionAccepted
	    	&& stateAccepted
	    	&& countyAccepted) 
	    {
	    	return true;
	    } else 
	    {
	    	return false;
	    }
    }
}
}