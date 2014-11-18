package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseTractQQEntity")]
public class LeaseTractQQ
{
    public var id:int;
    public var leaseTract:LeaseTract;
    public var tract:Tract;
    
    public function createCopy():LeaseTractQQ 
    {
    	var result:LeaseTractQQ = new LeaseTractQQ();
    	
    	result.tract = this.tract;
    	
    	return result;
    }
}
}
