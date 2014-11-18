package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.CoverageTractEntity")]
public class CoverageTract
{
    public var id:int;
    public var type:int;
    public var township:String;
    public var tdir:String;
    public var range:String;
    public var rdir:String;
    public var meridian:int;
    public var section:String;
    public var tractDescription:String;
    public var name:String;
    public var acres:Number = 0;
    public var coverageSet:ArrayCollection;
    public var county:County;
    
    public function toString():String {
    	var externals:String = "";
    	for each (var i:CoverageTractSet in coverageSet) {
    		externals += i.externalId + ",";
    	}  
    	return "Tract ID: " + id
    		+ ", Name: " + name	
    		+ ", Township: " + township	
    		+ ", TDir: " + tdir	
    		+ ", Range: " + range	
    		+ ", RDir: " + rdir	
    		+ ", Meridian: " + meridian	
    		+ ", Section: " + section	
    		+ ", Tract Description: " + tractDescription	
    		+ ", Acres: " + acres	
    		+ ", LTs: [" + externals + "]";	
    }
}
}
